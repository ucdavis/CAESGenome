using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using CAESGenome.Resources;
using Ionic.Zip;
using Tamir.SharpSsh;

namespace CAESGenome.Services
{
    public interface IPhredService
    {
        string SaveFiles(byte[] contents, string filename, out int fileId);
        void ScanFiles();
        void ExecuteValidation(int barcode);

        byte[] DownloadResults(Barcode barcode);
    }

    public class PhredService : IPhredService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private static readonly string PhredServer = ConfigurationManager.AppSettings["PhredServer"];
        private static readonly string PhredUsername = ConfigurationManager.AppSettings["PhredUsername"];
        private static readonly string PhredPassword = ConfigurationManager.AppSettings["PhredPassword"];
        private readonly string _storageLocation = ConfigurationManager.AppSettings["StorageLocation"];
        private readonly string _uploadLocation = ConfigurationManager.AppSettings["UploadLocation"];

        // ex. 2020717_A01.ab1
        private const string FilePattern = @"^[0123456789]+_[ABCDEFGH]{1}[0123456789]{2}\.";

        public PhredService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public string SaveFiles(byte[] contents, string filename, out int fileId)
        {
            fileId = 0;

            var barcodeId = -1;
            var row = 'A';
            var col = -1;

            ParseFileName(filename, out barcodeId, out row, out col);

            // invalid filename
            if (barcodeId == -1)
            {
                return FileUploadErrorKeys.InvalidFileName;
            }

            // load up the barcode, from db
            var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(barcodeId);

            if (barcode == null)
            {
                return FileUploadErrorKeys.InvalidBarcode;
            }

            // check db for existing file
            var existingFile = _repositoryFactory.BarcodeFileRepository.Queryable.FirstOrDefault(a => a.Barcode == barcode && a.WellColumn == col && a.WellRow == row);

            // check the file system
            if (File.Exists(string.Format(@"{0}\raw\{1}\{2}", _storageLocation, barcodeId, filename)) || existingFile != null)
            {
                return FileUploadErrorKeys.DuplicateFile;
            }

            if (WriteFile(filename, contents, string.Format(@"{0}\raw\{1}", _storageLocation, barcodeId)))
            {
                var bfile = new BarcodeFile()
                {
                    Barcode = barcode,
                    WellColumn = col,
                    WellRow = row,
                    Uploaded = true
                };

                _repositoryFactory.BarcodeFileRepository.EnsurePersistent(bfile);

                fileId = bfile.Id;

                return string.Empty;
            }

            return FileUploadErrorKeys.WriteError;
        }

        public void ScanFiles()
        {
            //var uploadLocation = @"\\do-files\cgfdata";
            //var uploadLocation = @"C:\Users\lai\Dropbox\Documents\Projects\Cgf\cgf\Cgf Data\data";

            foreach(var directory in Directory.EnumerateDirectories(_uploadLocation))
            {
                foreach(var file in Directory.EnumerateFiles(directory))
                {
                    var start = file.LastIndexOf('\\') + 1;
                    var filename = file.Substring(start);

                    var barcodeId = -1;
                    var row = 'A';
                    var col = -1;

                    ParseFileName(filename, out barcodeId, out row, out col);

                    if (barcodeId != -1)
                    {
                        // load up the barcode, from db
                        var barcode = _repositoryFactory.BarcodeRepository.GetNullableById(barcodeId);

                        if (barcode != null)
                        {
                            if (!Directory.Exists(string.Format(@"{0}\raw\{1}", _storageLocation, barcodeId)))
                            {
                                Directory.CreateDirectory(string.Format(@"{0}\raw\{1}", _storageLocation, barcodeId));
                            }

                            if (File.Exists(string.Format(@"{0}\raw\{1}\{2}", _storageLocation, barcodeId, filename)))
                            {
                                File.Delete(string.Format(@"{0}\raw\{1}\{2}", _storageLocation, barcodeId, filename));
                            }

                            // move the file into permanent storage, over write any existing files
                            File.Move(file, string.Format(@"{0}\raw\{1}\{2}", _storageLocation, barcodeId, filename));

                            // save the record into the database
                            var barcodeFile = barcode.BarcodeFiles.FirstOrDefault(a => a.WellColumn == col && a.WellRow == row);

                            if (barcodeFile == null)
                            {
                                barcodeFile = new BarcodeFile() {Barcode = barcode, WellColumn = col, WellRow = row, Uploaded = true };
                            }
                            else
                            {
                                // resets all the values so that phred runs again and displays the right values
                                barcodeFile.DateTimeValidated = null;
                                barcodeFile.Validated = false;
                                barcodeFile.Uploaded = true;
                                barcodeFile.Start = 0;
                                barcodeFile.End = 0;
                            }

                            _repositoryFactory.BarcodeFileRepository.EnsurePersistent(barcodeFile);
                        }
                    }
                }

                Directory.Delete(directory);
            }
        }

        private void ParseFileName(string filename, out int barcode, out char row, out int col)
        {            
            // check the filename
            var regex = new Regex(FilePattern);
            var match = regex.Match(filename);

            if (match.Success)
            {
                // parse the name
                barcode = Convert.ToInt32(filename.Substring(0, filename.IndexOf("_")));

                // split after the barcode
                var seperator = filename.Substring(filename.IndexOf("_") + 1);
                // remove the extension
                seperator = seperator.Substring(0, seperator.IndexOf("."));

                row = seperator[0];
                col = Convert.ToInt32(seperator.Substring(1));    
            }
            else
            {
                barcode = -1;
                row = 'A';
                col = -1;
            }
        }

        // write files to the storage locations
        private bool WriteFile(string filename, byte[] data, string path)
        {
            try
            {
                // check if the path exists
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                System.IO.File.WriteAllBytes(string.Format(@"{0}\{1}", path, filename), data);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ExecuteValidation(int barcode)
        {
            RunPhred(barcode);

            // update the records, to show that they've had validation run on them
            foreach(var bcf in _repositoryFactory.BarcodeFileRepository.Queryable.Where(a => a.Barcode.Id == barcode))
            {
                bcf.Validated = true;
                ValidatePhred(bcf);
                _repositoryFactory.BarcodeFileRepository.EnsurePersistent(bcf);
            }

            // update the barcode
            var bc = _repositoryFactory.BarcodeRepository.GetById(barcode);
            bc.DateTimeValidated = DateTime.Now;
            _repositoryFactory.BarcodeRepository.EnsurePersistent(bc);

            //ValidatePhred(barcode);
        }

        public byte[] DownloadResults(Barcode barcode)
        {
            var stream = new MemoryStream();

            // zip up the files and return them
            using (var zip = new ZipFile())
            {
                foreach(var file in Directory.EnumerateFiles(string.Format(@"{0}\raw\{1}", _storageLocation, barcode.Id), "*"))
                {
                    zip.AddFile(file, string.Empty);
                }

                zip.Save(stream);
            }

            return stream.ToArray();
        }

        // run phred
        private void RunPhred(int barcode)
        {
            // list of commands to execute
            // cp -R /mnt/cgfdata/raw/{barcode} /home/caesdev/raw
            // mkdir /home/caesdev/output/{barcode}
            // /opt/pkg/genome/bin/phred -id /home/caesdev/raw/{barcode} -qd /home/caesdev/output/{barcode}
            // cp -R /home/caesdev/output/{barcode} /mnt/cgfdata/output

            string output = null, error = null;

            var ssh = new SshExec(PhredServer, PhredUsername, PhredPassword);
            ssh.Connect();

            try
            {
                // copy in data
                ssh.RunCommand(string.Format(@"cp -R /mnt/cgfdata/raw/{0} /home/{1}/raw", barcode, PhredUsername));
                ssh.RunCommand(string.Format(@"mkdir /home/{0}/output/{1}", PhredUsername, barcode));

                // execute
                ssh.RunCommand(string.Format(@"/opt/pkg/genome/bin/phred -id /home/{0}/raw/{1} -qd /home/{0}/output/{1}", PhredUsername, barcode), ref output, ref error);

                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }

                // clean up
                ssh.RunCommand(string.Format(@"mv /home/{0}/output/{1} /mnt/cgfdata/output", PhredUsername, barcode));
                ssh.RunCommand(string.Format(@"rm /home/{0}/raw/{1}/*", PhredUsername, barcode));
                ssh.RunCommand(string.Format(@"rmdir /home/{0}/raw/{1}", PhredUsername, barcode));
            }
            catch
            {
                // automatic cleanup
                ssh.RunCommand(string.Format(@"rm /home/{0}/output/{1}/*", PhredUsername, barcode));
                ssh.RunCommand(string.Format(@"rmdir /home/{0}/output/{1}", PhredUsername, barcode));
                ssh.RunCommand(string.Format(@"rm /home/{0}/raw/{1}/*", PhredUsername, barcode));
                ssh.RunCommand(string.Format(@"rmdir /home/{0}/raw/{1}", PhredUsername, barcode));
            }

            ssh.Close();
        }

        // run the validation, calculate the quality results
        private void ValidatePhred(BarcodeFile bcf)
        {
            //var files = _repositoryFactory.BarcodeFileRepository.Queryable.Where(a => a.Barcode.Id == bcf.Barcode.Id && !a.Validated).ToList();

            var start = 0;
            var end = 0;

            var numbers = ReadPhredFile(bcf.Barcode.Id, bcf);

            FindIndexes(numbers, out start, out end);

            bcf.Start = start;
            bcf.End = end;
            bcf.DateTimeValidated = DateTime.Now;
            bcf.Validated = true;
        }

        private int[] ReadPhredFile(int barcode, BarcodeFile barcodeFile)
        {
            var path = string.Format(@"{0}\output\{1}\{2}", _storageLocation, barcode, barcodeFile.ValidationFileName);

            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path);
                lines[0] = string.Empty;
                var line = string.Join(" ", lines);

                var values = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var numbers = new int[values.Count()];
                for (var i = 0; i < values.Count(); i++)
                {
                    numbers[i] = Convert.ToInt32(values[i].Trim());
                }

                return numbers;
            }

            return new int[0];
        }
        
        // fixed values, for finding indexes
        private const int StartTrigger = 20;
        private const int WindowSize = 10;
        private const int EndTrigger = 20;

        private void FindIndexes(int[] numbers, out int start, out int end)
        {
            start = FindStartIndex(numbers);
            end = FindEndIndex(numbers, start);
        }
        private int FindStartIndex(int[] numbers)
        {
            var windowStart = 0;
            var openWindow = false;
            var windowCount = 0;

            // find the start index
            for (var i = 0; i < numbers.Length; i++)
            {
                // number meets criteria for open
                if (numbers[i] >= StartTrigger)
                {
                    // closed window, open up the window
                    if (!openWindow)
                    {
                        windowStart = i;
                        openWindow = true;
                    }

                    // increase window size
                    windowCount++;

                    // window satisfied
                    if (windowCount == WindowSize)
                    {
                        return windowStart;
                    }
                }

                // number below trigger, close the window
                if (numbers[i] < StartTrigger && openWindow)
                {
                    openWindow = false;
                    windowCount = 0;
                }
            }

            // found nothing
            return 0;
        }
        private int FindEndIndex(int[] numbers, int startIndex)
        {
            var windowStart = 0;
            var openWindow = false;
            var windowCount = 0;

            // find the start index to close
            for (var i = startIndex; i < numbers.Length; i++)
            {
                if (numbers[i] < EndTrigger)
                {
                    // closed window, open up the window
                    if (!openWindow)
                    {
                        windowStart = i;
                        openWindow = true;
                    }

                    // just count to increase window
                    windowCount++;

                    // window satisfied
                    if (windowCount >= WindowSize)
                    {
                        return windowStart;
                    }
                }

                if (numbers[i] >= EndTrigger && openWindow)
                {
                    openWindow = false;
                    windowCount = 0;
                }
            }

            return 0;
        }
    }

    public class PlateResult
    {
        public byte[] File { get; set; }
        public string Filename { get; set; }
    }
}