using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Account
{
    public class Conta
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
