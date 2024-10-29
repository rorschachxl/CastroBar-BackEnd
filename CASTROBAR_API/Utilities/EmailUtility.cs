using CASTROBAR_API.Dtos;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;


namespace CASTROBAR_API.Utilities
{
    public class EmailUtility : IEmailUtility
    {
        private readonly IConfiguration _config;
        public EmailUtility(IConfiguration config)
        {
            _config = config;
        }
        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));

            // Separar las direcciones de correo destinatarias por comas y agregarlas al campo "To"
            foreach (var destinatario in request.Addressee.Split(','))
            {
                email.To.Add(MailboxAddress.Parse(destinatario.Trim()));
            }

            email.Subject = request.Affair;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = request.Contain,
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(
                _config.GetSection("Email:Host").Value,
                Convert.ToInt32(_config.GetSection("Email:Port").Value),
                SecureSocketOptions.StartTls
            );

            smtp.Authenticate(_config.GetSection("Email:UserName").Value, _config.GetSection("Email:PassWord").Value);

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
