using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace FIT5032AssignmentV1.Utils
{
    public class EmailSender
    {
        private const String API_KEY = "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";

        public void SendSingle(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("yixu4928@gmail.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        public void SendAttach(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("yixu4928@gmail.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var bytes = File.ReadAllBytes("E:\\Visual Studio repository\\AttatchEmailTest.docx");
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("AttatchEmailTest", file);
            var response = client.SendEmailAsync(msg);
        }

        public void SendBulkAttachEmial(List<String> toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("yixu4928@gmail.com", "FIT5032 Example Email User");
            List<EmailAddress> to = new List<EmailAddress>();
            foreach(String email in toEmail) {
                to.Add(new EmailAddress(email,""));
            }
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent);
            var bytes = File.ReadAllBytes("E:\\Visual Studio repository\\AttatchEmailTest.docx");
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("AttatchEmailTest", file);
            var response = client.SendEmailAsync(msg);
        }
    }
}