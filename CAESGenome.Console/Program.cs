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
                System.Console.WriteLine("Provide a location to process");
                return;
            }
        
            ImportFiles(args[0]);
        }

        private static void ImportFiles(string uploadLocation)
        {
            // path
            var updates = new List<TransferData>();

            System.Console.WriteLine("Starting scan of directory");

            foreach(var directory in Directory.EnumerateDirectories(uploadLocation))
            {
                System.Console.WriteLine(directory);

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
            
            System.Console.WriteLine("Completed scan of directory");
            System.Console.WriteLine("{0} files processed", updates.Count);

            var db = new DataDataContext();

            var total = updates.Count;
            var counter = 0;

            System.Console.WriteLine("Starting copy of data");
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

            System.Console.WriteLine("Completed copy of data");
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

        
    }

    public class TransferData
    {
        public string Source { get; set; }
        public string Destination { get; set; }

        public int Barcode { get; set; }
    }
}
