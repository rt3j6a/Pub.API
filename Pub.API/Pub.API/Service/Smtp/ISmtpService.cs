namespace Pub.API.Service.Smtp {
    public interface ISmtpService {
        Task SendAssignmentAfterEmail(string destinationEmail, string teamName);

        Task SendAssignmentHandledEmail(string destinationEmail, string teamName, bool isAccepted);

    }
}
