using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using CAESGenome.Core.Domain;
using CAESGenome.Core.Repositories;
using Tamir.SharpSsh;

namespace CAESGenome.Services
{
    public interface IPhredService
    {
        void PushToServer(string folderName, List<PlateResult> files);

        void ExecuteValidation(int barcode);
        void RunPhredValidation(int barcode);
    }

    public class PhredService : IPhredService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public static readonly string PhredServer = ConfigurationManager.AppSettings["PhredServer"];
        public static readonly string PhredUsername = ConfigurationManager.AppSettings["PhredUsername"];
        public static readonly string PhredPassword = ConfigurationManager.AppSettings["PhredPassword"];
        private string _storageLocation = ConfigurationManager.AppSettings["StorageLocation"];

        public PhredService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public void PushToServer(string folderName, List<PlateResult> files)
        {
            //var scp = new Scp(PhredServer, PhredPassword, PhredPassword);

            //var dest = string.Format("/home/caesdev/raw/{0}/", folderName);

            //foreach(var file in files)
            //{
            //    var ms = new MemoryStream(file.File);
            //}

            //scp.Close();

        }

        public void ExecuteValidation(int barcode)
        {
            RunPhred(barcode);

            // update the records, to show that they've had validation run on them
            foreach(var bcf in _repositoryFactory.BarcodeFileRepository.Queryable.Where(a => a.Barcode.Id == barcode))
            {
                bcf.Validated = true;
                _repositoryFactory.BarcodeFileRepository.EnsurePersistent(bcf);
            }

            ValidatePhred(barcode);
        }

        public void RunPhredValidation(int barcode)
        {
            ValidatePhred(barcode);
        }

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

        private void ValidatePhred(int barcode)
        {
            foreach (var bcf in _repositoryFactory.BarcodeFileRepository.Queryable.Where(a => a.Barcode.Id == barcode))
            {
                var start = 0;
                var end = 0;

                var numbers = ReadPhredFile(barcode, bcf);

                FindIndexes(numbers, out start, out end);

                var quality = new PhredQuality() { Barcode = bcf.Barcode, WellColumn = bcf.WellColumn, WellRow = bcf.WellRow, Start = start, End = end };
                _repositoryFactory.PhredQualityRepository.EnsurePersistent(quality);
            }
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
        private const int StartScore = 20;
        private const int StartWindowSize = 7;
        private const int EndScore = 15;
        private const int EndWindowSize = 10;
        private const int DoubleCheckWindowSize = 50;
        
        private void FindIndexes(int[] numbers, out int start, out int end)
        {
            var curr = 0;
            var numsCount = numbers.Length;

            start = curr;
            end = curr;

            // find start
            while(curr < numsCount)
            {
                var i = 0;

                while(i < StartWindowSize && (curr + i) < numsCount)
                {
                    if (numbers[curr + i] < StartScore)
                    {
                        curr++;
                        i = 0;
                    }
                    else
                    {
                        i++;
                    }
                }

                start = curr;
                break;
            }

            // find end
            while (curr < numsCount)
            {
                var i = 0;

                while (i < EndWindowSize)
                {
                    if (curr + i >= numsCount)
                    {
                        break;
                    }
                    
                    if (numbers[curr + i] > EndScore)
                    {
                        curr++;
                        i = 0;
                    }
                    else
                    {
                        i++;
                    }
                }

                end = curr;

                var continueFlag = false;
                var currAhead = curr;

                while (currAhead + DoubleCheckWindowSize < numsCount)
                {
                    var j = 0;
                    while(j < DoubleCheckWindowSize)
                    {
                        if (numbers[currAhead + j] < StartScore)
                        {
                            currAhead++;
                            break;
                        }
                        else
                        {
                            j++;
                        }
                    }

                    if (j == DoubleCheckWindowSize)
                    {
                        curr = currAhead + DoubleCheckWindowSize;
                        continueFlag = true;
                        break;
                    }
                }

                if (!continueFlag)
                {
                    break;
                }
            }
        }
    }

    public class PlateResult
    {
        public byte[] File { get; set; }
        public string Filename { get; set; }
    }
}