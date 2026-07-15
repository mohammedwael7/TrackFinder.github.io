using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace TrackFinder.Providers.Common.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration config, ILogger<EmailSender> logger)
        {
            _config = config;
            _logger = logger;
        }

        public bool SendEmail(string toEmail, string htmlBody, string subject = "Track Finder Notification")
        {
            var smtpHost = _config["Email:SmtpHost"] ?? throw new InvalidOperationException("Email:SmtpHost not configured.");
            var smtpPort = int.Parse(_config["Email:SmtpPort"] ?? "587");
            var fromAddress = _config["Email:FromAddress"] ?? throw new InvalidOperationException("Email:FromAddress not configured.");
            var fromName = _config["Email:FromName"] ?? "Track Finder";
            var username = _config["Email:Username"] ?? throw new InvalidOperationException("Email:Username not configured.");
            var password = _config["Email:Password"] ?? throw new InvalidOperationException("Email:Password not configured.");

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromName, fromAddress));
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = htmlBody
                };

                using var smtp = new SmtpClient();
                smtp.Connect(smtpHost, smtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(username, password);
                smtp.Send(message);
                smtp.Disconnect(true);

                _logger.LogInformation("Email sent to {Email} | Subject: {Subject}", toEmail, subject);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", toEmail);
                return false;
            }
        }
    }
}