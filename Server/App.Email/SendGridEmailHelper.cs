using App.Common.Config;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Linq;
using System.Threading.Tasks;

namespace App.Email
{
    public class SendGridEmailHelper : IEmailHelper
    {
        private readonly string apiKey;

        private readonly string defaultEmails;

        public SendGridEmailHelper(ConfigOptions<SendGridOptions> config)
        {
            apiKey = config.Options.ApiKey;
            defaultEmails = config.Options.Emails;
        }

        public async Task SendEmail(string from, string[] to, string body, string subject)
        {
            var client = new SendGridClient(apiKey);

            var fromAddress = new EmailAddress(from);
            var toEmails = to ?? defaultEmails.Split(',');
            var toAddresses = toEmails.Select(e => new EmailAddress(e)).ToList();
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(fromAddress, toAddresses, subject, body, body);

            await client.SendEmailAsync(msg);
        }
    }
}
