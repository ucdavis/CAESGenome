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
                scp.SendFile(ms, file.Filename, dest);
            }

            scp.Close();

        }
    }

    public class PlateResult
    {
        public byte[] File { get; set; }
        public string Filename { get; set; }
    }
}