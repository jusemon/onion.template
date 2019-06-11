using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Proyect.Domain.Entities.Config
{
    public class EmailConfig
    {
        public string Server { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Sender { get; set; }
    }
}
