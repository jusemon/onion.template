namespace Company.Project.Domain.Services.Email
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

    /// <summary>
    /// Email Service class. 
    /// </summary>
    /// <seealso cref="Company.Project.Domain.Interfaces.Email.IEmailService" />
    public class EmailService : IEmailService
    {
        /// <summary>
        /// The sender
        /// </summary>
        private readonly string sender;

        /// <summary>
        /// The client
        /// </summary>
        private readonly SmtpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="emailConfig">The email configuration.</param>
        public EmailService(EmailConfig emailConfig)
        {
            this.sender = emailConfig.Sender;

            this.client = new SmtpClient(emailConfig.Server);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(emailConfig.Username, emailConfig.Password);
        }

        /// <summary>
        /// Sends the email to the specified destinatary.
        /// </summary>
        /// <param name="destinatary">The destinatary.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public void Send(string destinatary, string subject, string body)
        {
            this.Send(destinatary, subject, body, false);
        }

        /// <summary>
        /// Sends the email to the specified destinataries.
        /// </summary>
        /// <param name="destinataries">The destinataries.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public void Send(IEnumerable<string> destinataries, string subject, string body)
        {
            this.Send(destinataries, subject, body, false);
        }

        /// <summary>
        /// Sends the email to the specified destinatary.
        /// </summary>
        /// <param name="destinatary">The destinatary.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public void Send(string destinatary, string subject, string body, bool isHtml)
        {
            this.Send(new[] { destinatary }, subject, body, isHtml);
        }

        /// <summary>
        /// Sends the email to the specified destinataries.
        /// </summary>
        /// <param name="destinataries">The destinataries.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public void Send(IEnumerable<string> destinataries, string subject, string body, bool isHtml)
        {
            this.Send(destinataries, subject, body, null, isHtml);
        }

        /// <summary>
        /// Sends the email to the specified destinatary.
        /// </summary>
        /// <param name="destinatary">The destinatary.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public void Send(string destinatary, string subject, string body, dynamic parameters, bool isHtml)
        {
            this.Send(new[] { destinatary }, subject, body, parameters, isHtml);
        }

        /// <summary>
        /// Sends the email to the specified destinataries.
        /// </summary>
        /// <param name="destinataries">The destinataries.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
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
