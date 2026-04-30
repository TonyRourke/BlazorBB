using BlazorAppBB.Components.Account.Pages.Manage;
using BlazorAppBB.Data;
using BlazorAppBB.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Org.BouncyCastle.Security;
using System.Data;
using System.Net.NetworkInformation;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BlazorAppBB.Services
{
    public class MyEmailSender : IEmailSender<ApplicationUser>
    {
        private readonly IConfiguration _config;

        public MyEmailSender(IConfiguration configuration)
        {
            _config = configuration;
        }

        public Task SendConfirmationLinkAsync(ApplicationUser user, string emailto, string confirmationLink)
        {
            var eSender = new EmailSenderX(_config);

            string subject = "Confirmation Link";

            eSender.SendEmail(emailto, subject, confirmationLink); 
            return Task.CompletedTask;
        }
                
        public Task SendPasswordResetCodeAsync(ApplicationUser user, string emailto, string resetCode)
        {
            var eSender = new EmailSenderX(_config);

            string subject = "Reset Code";

            eSender.SendEmail(emailto, subject, resetCode); 
            return Task.CompletedTask;
        }

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string emailto, string resetLink)
        {
            var eSender = new EmailSenderX(_config);

            string subject = "Reset Password";

            eSender.SendEmail(emailto, subject, resetLink); 
            return Task.CompletedTask;
        }
    }

    public class EmailSenderX 
    {
       
        private readonly IConfiguration _config;
        
        public EmailSenderX(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void SendEmail(string emailto, string subject, string message)
        {
            var eReq = new EmailRequest();

            var MySecrets = _config.GetSection("MySecrets") ?? throw new InvalidOperationException("Section not found.");
            var epassword = MySecrets.GetRequiredSection("ePassword").Value ?? throw new InvalidOperationException("Section not found.");
            var ename = MySecrets.GetRequiredSection("eName").Value ?? throw new InvalidOperationException("Section not found.");
            var eservice = MySecrets.GetRequiredSection("eService").Value ?? throw new InvalidOperationException("Section not found.");

            eReq.Body = "Confirm registration using this link : " + message;
            eReq.To = emailto;
            eReq.Subject = subject;

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(ename));
                email.To.Add(MailboxAddress.Parse(eReq.To));
                email.Subject = eReq.Subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = eReq.Body
                };
                               
                using var smtp = new SmtpClient();

                smtp.Connect(eservice, 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(ename, epassword);
                smtp.Send(email);
                smtp.Disconnect(true);

                eReq.Error = "Sent";
            }
            catch (Exception ex)
            {
                var result = $"Error: {ex.Message}";
                eReq.Error = result;
            }
        }
    }
}
