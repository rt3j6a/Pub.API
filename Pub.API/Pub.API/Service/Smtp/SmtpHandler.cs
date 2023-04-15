using System.Net;
using System.Net.Mail;

namespace Pub.API.Service.Smtp {
    public class SmtpHandler :IDisposable{

        private SmtpClient client;

        private string? username;
        private string? password;
        private int? port;
        private string? email;
        private string? host;

        public SmtpHandler(IConfiguration configuration)
        {

             port= int.Parse(configuration["Smtp:Port"] ?? "0");
             username = configuration["Smtp:Username"];
             password = configuration["Smtp:Password"];
             email = configuration["Smtp:Email"];
            host = configuration["Smtp:Host"];

            var credentials = new NetworkCredential(userName: username, password: password);

            client = new SmtpClient(host) { 
                Port=port.Value,
                Credentials=credentials,
                EnableSsl=true
            };
        }

        public virtual void Dispose() {
            if (client != null) {
                client.Dispose();
            }
        }

        public async Task SendMail(string subject, string body, string destinationEmail) {

            if (client == null) return;

            var mailMessage = new MailMessage { 
                From=new MailAddress(email),
                Subject=subject,
                Body=body,
                IsBodyHtml=true
            };

            mailMessage.To.Add(new MailAddress(destinationEmail));

            await client.SendMailAsync(mailMessage);
        }



    }
}
