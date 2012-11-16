using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CAESGenome.Console
{
    class Program
    {
        private const string _storageLocation = @"\\agdean6\cru\backup\cgfdata\raw";

        static void Main(string[] args)
        {
            RenameFolders(args);
        }

        private static void RenameFolders(string[] args)
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

            // rename the folders
            foreach(var u in updates)
            {
                // check for the destination
                if (!Directory.Exists(u.Destination))
                {
                    Directory.CreateDirectory(u.Destination);
                }

                if (db.Barcodes.Any(a => a.Id == u.Barcode))
                {
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
                                bf = new BarcodeFile() { DateTimeUploaded = DateTime.Now, BarcodeId = barcode, Uploaded = true, WellColumn = col, WellRow = row };
                                db.BarcodeFiles.InsertOnSubmit(bf);
                            }
                            else
                            {
                                bf.Uploaded = true;
                                bf.DateTimeUploaded = DateTime.Now;
                            }

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
    }

    public class TransferData
    {
        public string Source { get; set; }
        public string Destination { get; set; }

        public int Barcode { get; set; }
    }
}
