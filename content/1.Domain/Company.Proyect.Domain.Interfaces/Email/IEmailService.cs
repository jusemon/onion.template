namespace Company.Proyect.Domain.Interfaces.Email
{
    using System.Collections.Generic;

    public interface IEmailService
    {
        void Send(string destinatary, string subject, string body);

        void Send(IEnumerable<string> destinataries, string subject, string body);

        void Send(string destinatary, string subject, string body, bool isHtml);

        void Send(IEnumerable<string> destinataries, string subject, string body, bool isHtml);

        void Send(string destinatary, string subject, string body, dynamic parameters, bool isHtml);

        void Send(IEnumerable<string> destinataries, string subject, string body, dynamic parameters, bool isHtml);
    }
}
