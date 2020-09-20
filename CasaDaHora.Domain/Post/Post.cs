using CasaDaHora.Domain.Account;
using CasaDaHora.Domain.Amigo;
using CasaDaHora.Domain.Comment;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace CasaDaHora.Domain.Post
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Texto { get; set; }
        public string UrlFoto { get; set; }
        public Guid AmigoDomainId { get; set; }

        //public Accounty Accounty { get; set; }
        //public List<Comment> Comments { get; set; }
        //public Comment Comments { get; set; }

        public Post()
        {

        }
        public Post(string _Texto, string _UrlFoto)
        {
            Texto = _Texto;
            UrlFoto = _UrlFoto;
        }

        public Post(Guid _Id, string _Texto, string _UrlFoto)
        {
            Id = _Id;
            Texto = _Texto;
            UrlFoto = _UrlFoto;
        }
        public Post(Guid _Id, string _Texto, string _UrlFoto, Guid _AmigoDomainId)
        {
            Id = _Id;
            Texto = _Texto;
            UrlFoto = _UrlFoto;
            AmigoDomainId = _AmigoDomainId;
        }
    }

}
