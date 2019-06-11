namespace Company.Proyect.Domain.Services.Email
{
    using Entities.Config;
    using FluentEmail.Core;
    using FluentEmail.Core.Models;
    using FluentEmail.Razor;
    using FluentEmail.Smtp;
    using Interfaces.Email;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;

    public class EmailService : IEmailService
    {
        private readonly string sender;

        private readonly SmtpClient client;

        public EmailService(EmailConfig emailConfig)
        {
            this.sender = emailConfig.Sender;

            this.client = new SmtpClient(emailConfig.Server);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(emailConfig.Username, emailConfig.Password);
        }

        public void Send(string destinatary, string subject, string body)
        {
            this.Send(destinatary, subject, body, false);
        }

        public void Send(IEnumerable<string> destinataries, string subject, string body)
        {
            this.Send(destinataries, subject, body, false);
        }

        public void Send(string destinatary, string subject, string body, bool isHtml)
        {
            this.Send(new[] { destinatary }, subject, body, isHtml);
        }

        public void Send(IEnumerable<string> destinataries, string subject, string body, bool isHtml)
        {
            this.Send(destinataries, subject, body, null, isHtml);
        }

        public void Send(string destinatary, string subject, string body, dynamic parameters, bool isHtml)
        {
            this.Send(new[] { destinatary }, subject, body, parameters, isHtml);
        }

        public void Send(IEnumerable<string> destinataries, string subject, string body, dynamic parameters, bool isHtml)
        {
            Email.DefaultRenderer = new RazorRenderer();
            Email.DefaultSender = new SmtpSender(this.client);
            Email.From(this.sender)
                .To(destinataries.Select(destinatary => new Address(destinatary)).ToList())
                .Subject(subject)
                .UsingTemplate(body, parameters, isHtml)
                .Send();
        }
    }
}
