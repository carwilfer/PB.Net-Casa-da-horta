using CasaDaHora.Domain.Account;
using CasaDaHora.Domain.Amigo;
using CasaDaHora.Domain.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Text;

namespace CasaDaHora.Domain.Post
{
    public class Postagem
    {
        [Key]
        public Guid Id { get; set; }
        public string TextoFoto { get; set; }
        public string UrlFoto { get; set; }

        [ForeignKey("user_id")]
        public Guid AmigoDomainId { get; set; }

        //public Accounty Accounty { get; set; }
        //public List<Comment> Comments { get; set; }
        //public Comment Comments { get; set; }

        public Postagem()
        {

        }
        public Postagem(string _TextoFoto, string _UrlFoto)
        {
            TextoFoto = _TextoFoto;
            UrlFoto = _UrlFoto;
        }

        public Postagem(Guid _Id, string _TextoFoto, string _UrlFoto)
        {
            Id = _Id;
            TextoFoto = _TextoFoto;
            UrlFoto = _UrlFoto;
        }
        public Postagem(Guid _Id, Guid _AmigoDomainId, string _UrlFoto,  string _TextoFoto)
        {
            Id = _Id;
            TextoFoto = _TextoFoto;
            UrlFoto = _UrlFoto;
            AmigoDomainId = _AmigoDomainId;
        }
    }

}
