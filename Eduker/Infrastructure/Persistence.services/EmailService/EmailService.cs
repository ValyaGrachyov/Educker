using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Persistence.services.EmailService;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMessageAsync(string email, string subject, string message)
    {
        var from = new MailAddress(_configuration["Email"]!, "Eduker");

        var to = new MailAddress(email);

        var m = new MailMessage(from, to);
        m.Subject = subject;
        m.Body = message;
        m.IsBodyHtml = true;

        var smtp = new SmtpClient(_configuration["SMTP"], 587);
        smtp.Credentials = new NetworkCredential(_configuration["Email"], _configuration["EmailPassword"]);
        smtp.EnableSsl = true;
        smtp.Send(m);
        
    }
}