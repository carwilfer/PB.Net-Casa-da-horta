using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Amigo
{
    public class AmigoDomain
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime Datanascimento { get; set; }
        public string Password { get; set; }
        public virtual IList<AmigoDomain> Amigos { get; set; }
        public RoleDomain Role { get; set; }

    }
 
}
