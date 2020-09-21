using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CasaDaHora.Domain.Amigo
{
    public class AmigoDomain
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "Campo Nome deve ter no máximo 50 caracteres")]
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        [StringLength(50, ErrorMessage = "Campo Email deve ter no máximo 50 caracteres")]
        [Required(ErrorMessage = "Campo Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Campo Email não está em um formato correto")]
        public string Email { get; set; }
        public DateTime Datanascimento { get; set; }
        public string Password { get; set; }
        public string UrlFoto { get; set; }
        public virtual IList<AmigoDosAmigos> AmigosQueSeguem { get; set; }
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

        public enum Status
        {
            NAO_ATIVO,
            ATIVO,
            EM_CONFIRMACAO_EMAIL
        }

    }
 
}
