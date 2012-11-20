using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Tamir.SharpSsh;

namespace CAESGenome.Console
{
    class Program
    {
        private const string _storageLocation = @"\\agdean6\cru\backup\cgfdata\raw";

        static void Main(string[] args)
        {
            if (!args.Any())
            {
                return;
            }

            if (args[0] == "-i")
            {
                ImportFiles(args);    
            }

            if (args[0] == "-p")
            {
                // validate phred values
                PhredValiation();    
            }
            
        }

        private static void ImportFiles(string[] args)
        {
            if (!args.Any())
            {
                System.Console.WriteLine("Provide a location to process");
                return;
            }

            // path
            var uploadLocation = args[0];
            var updates = new List<TransferData>();

            foreach(var directory in Directory.EnumerateDirectories(uploadLocation))
            {
                var firstFile = Directory.EnumerateFiles(directory).FirstOrDefault();
                if (firstFile != null)
                {
                    var filename = firstFile.Substring(firstFile.LastIndexOf('\\') + 1);

                    int barcode, col;
                    char row;
                    ParseFileName(filename, out barcode, out row, out col);

                    updates.Add(new TransferData() { Barcode = barcode, Source = directory, Destination = string.Format("{0}\\{1}", _storageLocation, barcode) });
                }
            }

            var db = new DataDataContext();

            var total = updates.Count;
            var counter = 0;

            // rename the folders
            foreach(var u in updates)
            {
                if (db.Barcodes.Any(a => a.Id == u.Barcode))
                {
                    // check for the destination
                    if (!Directory.Exists(u.Destination))
                    {
                        Directory.CreateDirectory(u.Destination);
                    }
                    
                    foreach (var file in Directory.EnumerateFiles(u.Source))
                    {
                        try
                        {
                            var start = file.LastIndexOf('\\') + 1;
                            var filename = file.Substring(start);

                            //System.Console.WriteLine(file);
                            //System.Console.WriteLine(filename);
                            //System.Console.WriteLine(u.Barcode);
                            //System.Console.WriteLine(string.Format("{0}\\{1}", u.Destination, filename));
                            //System.Console.WriteLine("-----------------------");

                            if (File.Exists(string.Format("{0}\\{1}", u.Destination, filename)))
                            {
                                File.Delete(string.Format("{0}\\{1}", u.Destination, filename));
                            }

                            File.Move(string.Format("{0}\\{1}", u.Source, filename), string.Format("{0}\\{1}", u.Destination, filename));

                            int barcode, col;
                            char row;
                            ParseFileName(filename, out barcode, out row, out col);

                            var bf = db.BarcodeFiles.FirstOrDefault(a => a.BarcodeId == barcode && a.WellColumn == col && a.WellRow == row);
                            if (bf == null)
                            {
                                bf = new BarcodeFile() { DateTimeUploaded = DateTime.Now, BarcodeId = barcode, Uploaded = true, Validated = false, WellColumn = col, WellRow = row };
                                db.BarcodeFiles.InsertOnSubmit(bf);
                            }
                            else
                            {
                                bf.Uploaded = true;
                                bf.Validated = false;
                                bf.DateTimeUploaded = DateTime.Now;
                            }

                            counter++;
                            System.Console.Clear();
                            System.Console.WriteLine(string.Format("{0}% Completed File Import",counter / total));
                            System.Console.WriteLine(string.Format("Processed {0}", filename));
                        }
                        catch
                        {
                        }
                    }

                    Directory.Delete(u.Source);
                }
                
                
            }

            db.SubmitChanges();
        }

        private const string FilePattern = @"^[0123456789]+_[ABCDEFGH]{1}[0123456789]{2}\.";
        private static void ParseFileName(string filename, out int barcode, out char row, out int col)
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

        private static void PhredValiation()
        {
            List<int> barcodes;

            using(var db = new DataDataContext())
            {
                barcodes = db.Barcodes.Where(a => a.BarcodeFiles.Any(b => b.Uploaded && !b.Validated)).Select(a => a.Id).ToList();    
            }

            var total = barcodes.Count();
            var counter = 0;

            foreach(var barcode in barcodes)
            {
                using (var db = new DataDataContext())
                {
                    RunPhred(barcode);
                    
                    foreach (var bcf in db.BarcodeFiles.Where(a => a.BarcodeId == barcode))
                    {
                        ValidatePhred(bcf);
                    }

                    counter++;
                    System.Console.Clear();
                    System.Console.WriteLine(string.Format("{0}% Completed Phred Validation", counter / total));
                    System.Console.WriteLine(string.Format("Processed {0}", barcode));

                    db.SubmitChanges();
                }    
            }
        }

        private static readonly string PhredServer = ConfigurationManager.AppSettings["PhredServer"];
        private static readonly string PhredUsername = ConfigurationManager.AppSettings["PhredUsername"];
        private static readonly string PhredPassword = ConfigurationManager.AppSettings["PhredPassword"];

        // run phred
        private static void RunPhred(int barcode)
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
        private static void ValidatePhred(BarcodeFile bcf)
        {
            var start = 0;
            var end = 0;

            var numbers = ReadPhredFile(bcf.Barcode.Id, bcf);

            FindIndexes(numbers, out start, out end);

            bcf.Start = start;
            bcf.End = end;
            bcf.DateTimeValidated = DateTime.Now;
            bcf.Validated = true;
        }

        private static int[] ReadPhredFile(int barcode, BarcodeFile barcodeFile)
        {
            var path = string.Format(@"{0}\output\{1}\{2}", _storageLocation, barcode, ValidationFileName(barcodeFile));

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
        private static string ValidationFileName(BarcodeFile bcf)
        {
            if (bcf.WellColumn < 10)
            {
                return string.Format("{0}_{1}0{2}.ab1.qual", bcf.Barcode.Id, bcf.WellRow, bcf.WellColumn);
            }

            return string.Format("{0}_{1}{2}.ab1.qual", bcf.Barcode.Id, bcf.WellRow, bcf.WellColumn);
        }

        // fixed values, for finding indexes
        private const int StartTrigger = 20;
        private const int WindowSize = 10;
        private const int EndTrigger = 20;

        private static void FindIndexes(int[] numbers, out int start, out int end)
        {
            start = FindStartIndex(numbers);
            end = FindEndIndex(numbers, start);
        }
        private static int FindStartIndex(int[] numbers)
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
        private static int FindEndIndex(int[] numbers, int startIndex)
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

    public class TransferData
    {
        public string Source { get; set; }
        public string Destination { get; set; }

        public int Barcode { get; set; }
    }
}
