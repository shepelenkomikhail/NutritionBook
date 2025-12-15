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
        var host = "Smtp:Host";
        var port = "Smtp:Port";
        var pass = "Smtp:Pass";
        var user = "Smtp:User";
        var fromEmail = "Smtp:FromEmail";
        var displayName = "Smtp:DisplayName";
        
        var smtpClient = new SmtpClient(_config[host]!)
        {
            Port = int.Parse(_config[port]!),
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_config[user]!, _config[pass]!),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_config[fromEmail]!, _config[displayName]!),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage);
    }
}