using MimeKit;
using MailKit.Net.Smtp;
using EStore.Controllers.RequestModels;

namespace Estore.Services
{
    public interface IEmailingService
    {
        Task SendEmailAsync(EmailRequest request);
    }
    public class EmailingService : IEmailingService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _emailFromAddress;
        private readonly string _appPassword;
        private readonly IConfiguration _configuration;

        public EmailingService(IConfiguration configuration)
        {
            _configuration = configuration;
            _emailFromAddress = _configuration.GetSection("smtp")["email"] ?? throw new Exception();
            _appPassword = _configuration.GetSection("smtp")["AppKey"] ?? throw new Exception();
        }

        public async Task SendEmailAsync(EmailRequest request)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Seif-Elden Mohamed", _emailFromAddress));
            message.To.Add(new MailboxAddress(request.RecipientName, request.To));
            message.Bcc.Add(new MailboxAddress("Seif-Elden Mohamed", _emailFromAddress));
            message.Subject = request.Subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = request.Body
            };
            message.Body = bodyBuilder.ToMessageBody();

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_emailFromAddress, _appPassword);
                await smtpClient.SendAsync(message);                            
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
