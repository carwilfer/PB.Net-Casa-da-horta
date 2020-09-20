using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Amigo
{
    public class RoleDomain
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<AmigoDomain> Amigo { get; set; }
    }
}
