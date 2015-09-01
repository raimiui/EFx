using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;

namespace EFx.Model.Senders
{
    public class EmailSender : IDocumentSender
    {
        public void Send(IList<FileInfo> scannedFiles)
        {
            try
            {
                var mail = new MailMessage();
                var smtpClient = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(ConfigurationManager.AppSettings["from_email_address"]);
                mail.To.Add(ConfigurationManager.AppSettings["to_email_address"]);
                mail.Subject = "Scanned doc 2";
                mail.Body = "Sveikas, maushi!";

                foreach (var scannedFile in scannedFiles)
                    mail.Attachments.Add(new Attachment(scannedFile.FullName));

                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["EmailLogin"], ConfigurationManager.AppSettings["EmailPassword"]);
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
                Console.WriteLine("Mail sent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();
        }
    }
}