using System.Linq;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Core.Resources;

namespace CAESGenome.Services
{
    public interface IBarcodeService
    {
        void AdvanceStage(IRepositoryFactory repositoryFactory, Barcode barcode, UserJob userJob);
        void AdvanceAllBarcodes(IRepositoryFactory repositoryFactory, UserJob userJob, Stage stage);
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
            var barcodes = userJob.UserJobPlates.SelectMany(a => a.Barcodes).Where(a => a.Stage == stage);

            if (barcodes.Any())
            {
                foreach(var bc in barcodes)
                {
                    AdvanceStage(repositoryFactory, bc, userJob);
                }
            }
        }
    }
}