using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace App.Email
{
    public class SmtpEmailHelper : IEmailHelper
    {
        private readonly SmtpClient client;

        public SmtpEmailHelper(string host, int port, string user, string pass)
        {
            client = new SmtpClient(host, port);
            if (!string.IsNullOrEmpty(user))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(user, pass);
            }
            else
            {
                client.UseDefaultCredentials = true;
            }
        }

        public async Task SendEmail(string from, string[] to, string body, string subject)
        {
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(from)
            };
            foreach (var adr in to)
            {
                mailMessage.To.Add(adr);
            }

            mailMessage.Body = body;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;

            await client.SendMailAsync(mailMessage);
        }
    }
}
