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
        public string UrlFoto { get; set; }
        public virtual IList<AmigoDomain> AmigosQueSeguem { get; set; }
        public RoleDomain Role { get; set; }


        public AmigoDomain()
        {
            
        }

        public AmigoDomain(string _Nome, string _Sobrenome, string _Email, DateTime _Datanascimento, string _Password)
        {
            Nome = _Nome;
            Sobrenome = _Sobrenome;
            Email = _Email;
            Datanascimento = _Datanascimento;
            Password = _Password;
        }
        public AmigoDomain(Guid _Id, string _Nome, string _Sobrenome, string _Email, DateTime _Datanascimento, string _Password)
        {
            Id = _Id;
            Nome = _Nome;
            Sobrenome = _Sobrenome;
            Email = _Email;
            Datanascimento = _Datanascimento;
            Password = _Password;
        }

    }
 
}
