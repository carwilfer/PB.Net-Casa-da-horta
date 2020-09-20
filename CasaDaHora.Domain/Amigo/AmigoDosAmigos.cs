using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Amigo
{
    public class AmigoDosAmigos
    {
        public Guid AmigoDosAmigosId { get; set; }
        public Guid AmigoDomainId { get; set; }
        public AmigoDomain AmigoDomainSeguidor { get; set; }
    }
}
