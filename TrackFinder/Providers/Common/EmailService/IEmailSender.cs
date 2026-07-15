namespace TrackFinder.Providers.Common.EmailService
{
    public interface IEmailSender
    {
        bool SendEmail(string toEmail, string htmlBody, string subject = "Track Finder Notification");
    }
}