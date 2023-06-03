namespace Persistence.services.EmailService;

public interface IEmailService
{
    Task SendMessageAsync(string email, string subject, string message);
}