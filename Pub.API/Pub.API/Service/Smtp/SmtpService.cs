namespace Pub.API.Service.Smtp {
    public class SmtpService :ISmtpService {

        private readonly IConfiguration configuration;

        public SmtpService(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task SendAssignmentAfterEmail(string destinationEmail, string teamName) {
            SmtpHandler handler = new SmtpHandler(configuration);

            await handler.SendMail(SmtpTextSchemes.Subject.MakeAssignmentAfterSubject(teamName),
                                   SmtpTextSchemes.Body.MakeAssignmentAfterBody(teamName),
                                   destinationEmail);
            handler.Dispose();
        }

        public async Task SendAssignmentHandledEmail(string destinationEmail, string teamName, bool isAccepted) {
            SmtpHandler handler=new SmtpHandler(configuration);

            await handler.SendMail(SmtpTextSchemes.Subject.MakeAssignmentHandledSubject(teamName),
                                   SmtpTextSchemes.Body.MakeAssignmentHandledBody(teamName,isAccepted),
                                   destinationEmail);
            handler.Dispose();
        }
    }
}
