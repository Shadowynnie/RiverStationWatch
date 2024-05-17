using Microsoft.AspNetCore.Identity.UI.Services;
using SQLitePCL;
using System.Net;
using System.Net.Mail;

namespace RiverStationWatch.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _log;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _log = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body) 
        {
            foreach(var recipient in to.Split(';')) 
            {
                using (var mail = new MailMessage()) 
                {
                    mail.From = new MailAddress("RiverWatch@example.com"); // Replace with your sender email adress
                    mail.To.Add(recipient);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    try
                    {
                        using(var smtp = new SmtpClient("localhost", 1025)) // Replace with your SMTP sender details
                        {   //For testing, we are using MailHog instead of real SMTP server
                            //smtp.Port = 587;
                            //smtp.Credentials = new NetworkCredential("noreply@codeclimber.cz", "Alia2022+");
                            //smtp.EnableSsl = true;
                            smtp.Timeout = 10000; // Set the timeout in milliseconds
                            await smtp.SendMailAsync(mail);
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.LogError(ex.Message);
                        Console.WriteLine($"Error sending email to {recipient}: {ex.Message}");
                        throw;
                    }
                }
            }
        }
    }
}
