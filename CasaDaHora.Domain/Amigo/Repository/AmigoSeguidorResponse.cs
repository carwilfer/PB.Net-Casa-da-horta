using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Amigo.Repository
{
    public class AmigoSeguidorResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime Datanascimento { get; set; }
        public string UrlFoto { get; set; }
    }
}
