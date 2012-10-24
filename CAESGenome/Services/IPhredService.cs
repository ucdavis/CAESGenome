using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Tamir.SharpSsh;

namespace CAESGenome.Services
{
    public interface IPhredService
    {
        void PushToServer(string folderName, List<PlateResult> files);

        void ExecuteValidation(int barcode);

    }

    public class PhredService : IPhredService
    {
        public static readonly string PhredServer = ConfigurationManager.AppSettings["PhredServer"];
        public static readonly string PhredUsername = ConfigurationManager.AppSettings["PhredUsername"];
        public static readonly string PhredPassword = ConfigurationManager.AppSettings["PhredPassword"];

        public void PushToServer(string folderName, List<PlateResult> files)
        {
            var scp = new Scp(PhredServer, PhredPassword, PhredPassword);

            var dest = string.Format("/home/caesdev/raw/{0}/", folderName);

            foreach(var file in files)
            {
                var ms = new MemoryStream(file.File);
            }

            scp.Close();

        }

        public void ExecuteValidation(int barcode)
        {
            // list of commands to execute
            // cp -R /mnt/cgfdata/raw/{barcode} /home/caesdev/raw
            // mkdir /home/caesdev/output/{barcode}
            // /opt/pkg/genome/bin/phred -id /home/caesdev/raw/{barcode} -qd /home/caesdev/output/{barcode}
            // cp -R /home/caesdev/output/{barcode} /mnt/cgfdata/output

            string output = null, error = null;

            var ssh = new SshExec(PhredServer, PhredUsername, PhredPassword);
            ssh.Connect();

            // copy in data
            ssh.RunCommand(string.Format(@"cp -R /mnt/cgfdata/raw/{0} /home/{1}/raw", barcode, PhredUsername));
            ssh.RunCommand(string.Format(@"mkdir /home/{0}/output/{1}", PhredUsername, barcode));           
            
            // execute
            ssh.RunCommand(string.Format(@"/opt/pkg/genome/bin/phred -id /home/{0}/raw/{1} -qd /home/{0}/output/{1}", PhredUsername, barcode), ref output, ref error);
            
            // clean up
            ssh.RunCommand(string.Format(@"mv /home/{0}/output/{1} /mnt/cgfdata/output", PhredUsername, barcode));
            ssh.RunCommand(string.Format(@"rm /home/{0}/raw/{1}/*", PhredUsername, barcode));
            ssh.RunCommand(string.Format(@"rmdir /home/{0}/raw/{1}", PhredUsername, barcode));

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }

            ssh.Close();
        }
    }

    public class PlateResult
    {
        public byte[] File { get; set; }
        public string Filename { get; set; }
    }
}