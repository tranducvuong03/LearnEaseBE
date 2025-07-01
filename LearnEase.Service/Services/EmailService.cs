using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LearnEase.Service.IServices;

namespace LearnEase.Service.Services
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _config;

		public EmailService(IConfiguration config)
		{
			_config = config;
		}

		public async Task SendWelcomeEmailAsync(string toEmail, string username)
		{
			var settings = _config.GetSection("EmailSettings");
			var from = settings["From"];
			var displayName = settings["DisplayName"];
			var smtpUser = settings["Username"];
			var smtpPass = settings["Password"];
			var smtpHost = settings["SmtpHost"];
			var smtpPort = int.Parse(settings["SmtpPort"]);

			var message = new MailMessage
			{
				From = new MailAddress(from, displayName),

				Subject = "🎉 Welcome to LearnEase!",
				IsBodyHtml = true
			};

			string htmlBody = $@"
    <div style='max-width:600px;margin:auto;padding:20px;font-family:Segoe UI,Roboto,sans-serif;background-color:#ffffff;border-radius:10px;box-shadow:0 4px 12px rgba(0,0,0,0.1);'>
        <div style='text-align:center;margin-bottom:20px;'>
            <img src='https://res.cloudinary.com/dwxqndule/image/upload/v1750497958/Logo-learnease_pjmxog.png' alt='LearnEase Logo' style='width:120px;margin-bottom:10px;'/>
            <h2 style='color:#2c3e50;margin:10px 0;'>👋 Welcome to LearnEase, {username}!</h2>
        </div>

        <p style='font-size:16px;color:#333;'>We're <strong>thrilled</strong> to have you on board 🎉.</p>
        <p style='font-size:15px;color:#555;line-height:1.6;'>
            LearnEase is your companion for smarter, simpler, and more efficient learning. You're now part of a growing community that values growth, innovation, and support.
        </p>

        <div style='text-align:center;margin:30px 0;'>
            <a href='https://learnease-psi.vercel.app/' target='_blank' 
               style='background-color:#3498db;color:#fff;padding:12px 24px;border-radius:6px;text-decoration:none;font-weight:bold;font-size:16px;display:inline-block;'>
                🚀 Start Exploring
            </a>
        </div>

        <hr style='border:none;border-top:1px solid #eee;margin:40px 0;' />

        <p style='font-size:13px;color:#888;text-align:center;'>
            This is an automated email from LearnEase. Please do not reply to this message.<br/>
            Need help? Contact us at <a href='mailto:learnease68@gmail.com' style='color:#3498db;'>support@learnease.com</a>
        </p>
    </div>
";


			message.Body = htmlBody;
			message.To.Add(toEmail);

			using var smtp = new SmtpClient(smtpHost, smtpPort)
			{
				Credentials = new NetworkCredential(smtpUser, smtpPass),
                Timeout = 5000,

                EnableSsl = true
			};

			await smtp.SendMailAsync(message);
		}
	}

}
