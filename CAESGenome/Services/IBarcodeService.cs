using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;
using Microsoft.VisualBasic;

namespace CAESGenome.Services
{
    public interface IBarcodeService
    {
        void AdvanceStage(IRepositoryFactory repositoryFactory, Barcode barcode, UserJob userJob);
        void AdvanceAllBarcodes(IRepositoryFactory repositoryFactory, UserJob userJob, Stage stage);

        void Print(Barcode barcode);
    }

    public class BarcodeService : IBarcodeService
    {
        private readonly int[] _largeplateJobs = new int[] { (int)JobTypeIds.BacterialClone, (int)JobTypeIds.PCRProduct, (int)JobTypeIds.UserRunSequencing
                                            , (int)JobTypeIds.PurifiedDna,(int)JobTypeIds.Sublibrary, (int)JobTypeIds.UserRuneGenotyping };
        private readonly int[] _directionalJobs = new int[] { (int)JobTypeIds.BacterialClone, (int)JobTypeIds.PCRProduct, (int)JobTypeIds.PurifiedDna };

        public void AdvanceStage(IRepositoryFactory repositoryFactory, Barcode barcode, UserJob userJob)
        {
            // barcode is complete and cannot be advanced
            if (barcode.Stage.IsComplete) return;

            var jobType = userJob.JobType;
            var stage = barcode.Stage;
            var plate = barcode.UserJobPlate;

            // by default only insert one new barcode
            var subPlates = 1;

            // jobs with large plate, gets broken down to 4 barcodes
            if (_largeplateJobs.Contains(jobType.Id) && userJob.PlateType == PlateTypes.ThreeEightyFour && stage.Order == 2)
            {
                subPlates = 4;
            }

            Primer primer1 = barcode.Primer;
            Primer primer2 = null;
            var directional = false;

            // if it's directional, you need a forward and back for each sub plate to generate
            if (_directionalJobs.Contains(jobType.Id))
            {
                // bacterial clone, 2nd stage, take the direction
                if (jobType.Id == (int)JobTypeIds.BacterialClone && stage.Order == 3 && userJob.UserJobBacterialClone.SequenceDirection == SequenceDirection.Backward)
                {
                    directional = true;
                    primer1 = userJob.UserJobBacterialClone.Primer1;
                    primer2 = userJob.UserJobBacterialClone.Primer2;
                }

                // frankly, I think this was old code that isn't used any more
                // pcr and purified dna take the direction at 1st stage
                if ((jobType.Id == (int)JobTypeIds.PCRProduct || jobType.Id == (int)JobTypeIds.PurifiedDna) && stage.Order == 2 && userJob.UserJobDna.SequenceDirection == SequenceDirection.Backward)
                {
                    directional = true;
                    primer1 = userJob.UserJobDna.Primer1;
                    primer2 = userJob.UserJobDna.Primer2;
                }
            }

            // load the next stage
            var nextStage = jobType.Stages.First(a => a.Order == stage.Order + 1);

            for (var i = 0; i < subPlates; i++)
            {
                // write the first plate
                var newBarcode1 = new Barcode() { SourceBarcode = barcode, Stage = nextStage, Primer = primer1, SubPlateId = i + 1};
                plate.AddBarcode(newBarcode1);

                if (directional)
                {
                    var newBarcode2 = new Barcode() { SourceBarcode = barcode, Stage = nextStage, Primer = primer2, SubPlateId = i + 1 };
                    plate.AddBarcode(newBarcode2);
                }
            }

            barcode.Done = true;

            repositoryFactory.UserJobPlateRepository.EnsurePersistent(plate);
        }

        public void AdvanceAllBarcodes(IRepositoryFactory repositoryFactory, UserJob userJob, Stage stage)
        {
            var barcodes = userJob.UserJobPlates.SelectMany(a => a.Barcodes).Where(a => a.Stage == stage && !a.Done);

            if (barcodes.Any())
            {
                foreach(var bc in barcodes)
                {
                    AdvanceStage(repositoryFactory, bc, userJob);
                }
            }
        }

        private readonly string _printer = ConfigurationManager.AppSettings["printer"];
        private readonly int _printerPort = Convert.ToInt32(ConfigurationManager.AppSettings["printerport"]);

        public void Print(Barcode barcode)
        {
            var address = IPAddress.Parse(_printer);
            var endPoint = new IPEndPoint(address, _printerPort);

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // all commands are for SATO CL412e Printer

            try
            {
                // open connection
                socket.Connect(endPoint);

                // escape command
                var cmd = Encoding.ASCII.GetBytes(Strings.Chr(27).ToString());

                // trigger the printer
                socket.Send(cmd);
                // start of printer command
                socket.Send(Encoding.ASCII.GetBytes("A"));
                socket.Send(cmd);
                // horizontal command
                socket.Send(Encoding.ASCII.GetBytes("H0210"));
                socket.Send(cmd);
                // vertical command
                socket.Send(Encoding.ASCII.GetBytes("V0010"));
                socket.Send(cmd);
                // BG is for printing barcode (code128)
                socket.Send(Encoding.ASCII.GetBytes(string.Format("BG03090 {0} . {1}", barcode.PlateName, barcode.Id)));
                socket.Send(cmd);
                socket.Send(Encoding.ASCII.GetBytes("H0250"));
                socket.Send(cmd);
                socket.Send(Encoding.ASCII.GetBytes("V0100"));
                // XM is font command for print CGF and data
                socket.Send(Encoding.ASCII.GetBytes(string.Format("XMCGF {0}", barcode.Id)));
                socket.Send(cmd);
                // print 1 label
                socket.Send(Encoding.ASCII.GetBytes("Q1"));
                socket.Send(cmd);
                // end of command
                socket.Send(Encoding.ASCII.GetBytes("Z"));

                // release the socket
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch
            {
                throw;
            }
        }
    }
}