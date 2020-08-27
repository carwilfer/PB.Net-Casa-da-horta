using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Account
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<Accounty> Accounts {get; set;} 
    }
}
