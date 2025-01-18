using Azure.Core;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Mail;
using System.Net;
using TasksEvaluation.Web.ViewModels;


namespace TasksEvaluation.Web
{
	public class EmailSender : IEmailSender
	{
		private readonly IConfiguration _configuration;

		public EmailSender(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{


            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            var client = new SmtpClient(emailSettings.Host, emailSettings.Port)
            {
                Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password),
                EnableSsl = true

            };
            client.Host = "smtp.gmail.com";
            client.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
            client.Port = 587;
            client.UseDefaultCredentials = false;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings.Username),
                Subject = subject,
                Body = htmlMessage,
                To = { new MailAddress(email) }
            };

            try
            {
                //client.Send(mailMessage.From.ToString(), mailMessage.To.ToString(), mailMessage.Subject.ToString(), mailMessage.Body);

                await client.SendMailAsync(mailMessage);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"SMTP Exception: {ex.Message}");
                throw;
            }
        }
	}
}
