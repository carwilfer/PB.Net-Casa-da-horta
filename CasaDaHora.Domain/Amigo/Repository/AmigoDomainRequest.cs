using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Amigo.Repository
{
    public class AmigoDomainRequest
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime Datanascimento { get; set; }
        public string Password { get; set; }

        public AmigoDomainRequest()
        {

        }

        public AmigoDomainRequest(string nome, string sobrenome, string email, DateTime datanascimento, string password)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Datanascimento = datanascimento;
            Password = password;
        }
    }
}
