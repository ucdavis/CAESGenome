using System.Configuration;
using System.Net.Mail;
using CAESGenome.Core.Domain;

namespace CAESGenome.Services
{
    /// <summary>
    /// Simple utility class to facilitate building email messages using basic templating for replacement
    /// of username, jobname and html line breaks, i.e. <br />, and then sending of email messages.
    /// </summary>
    public static class EmailService
    {
        public static void SendMessage(UserJob userJob)
        {
            var message = new System.Net.Mail.MailMessage()
            {
                From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], ConfigurationManager.AppSettings["FromEmailName"]),
                Subject = ConfigurationManager.AppSettings["MessageSubject"],
                Body = ConfigurationManager.AppSettings["MessageBody"]
            };
            message.Body = message.Body.Replace("$USERNAME", userJob.User.FullName);
            message.Body = message.Body.Replace("$JOBNAME", userJob.Name);
            message.Body = message.Body.Replace("$BR", "<br />");

            message.To.Add(new MailAddress(userJob.User.UserName, userJob.User.FullName));
            //message.To.Add(new MailAddress("kentaylor@ucdavis.edu", "Ken Taylor"));
            message.IsBodyHtml = true;

            var mailClient = new SmtpClient()
            {
                Host = ConfigurationManager.AppSettings["SmtpServer"]
            };

            mailClient.Send(message);}
    }
}