using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructur.EmailService
{
    public class EmailSender : IEmailSender
    {
        public EmaillSettings _emailSettings { get; }
        public EmailSender(IOptions<EmaillSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendEmail(EmailMessage email)
        {
            //use SendGrid
            var client = new SendGrid.SendGridClient(_emailSettings.ApiKey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            var response = await client.SendEmailAsync(message);  

            return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}
