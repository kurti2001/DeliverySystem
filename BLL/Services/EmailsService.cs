using System.Net;
using System.Net.Mail;

namespace BLL.Services
{
    public interface IEmailsService 
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
    internal class EmailsService: IEmailsService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var fromAddress = new MailAddress("done5640@gmail.com", "DeliverySystem");
            var toAddress = new MailAddress(toEmail);

            const string fromPassword = "yyrc zfky titu mcdv";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                try
                {
                    await smtp.SendMailAsync(message);
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine($"SMTP Exception: {ex.Message}");
                    throw; 
                }
            }

        }
    }
}