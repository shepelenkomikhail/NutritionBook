using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace NutritionalRecipeBook.Application.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;
    
    public EmailSender(IConfiguration config) => _config = config;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var host = _config["Smtp:Host"];
        var port = _config["Smtp:Port"];
        var pass = _config["Smtp:Pass"];
        var user = _config["Smtp:User"];
        var fromEmail = _config["Smtp:FromEmail"];
        var displayName = _config["Smtp:DisplayName"];
        
        var smtpClient = new SmtpClient(host)
        {
            Port = int.Parse(port!),
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(user, pass),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(fromEmail!, displayName!),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage);
    }
}