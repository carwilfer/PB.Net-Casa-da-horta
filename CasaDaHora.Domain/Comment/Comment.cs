using CasaDaHora.Domain.Account;
using CasaDaHora.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Comment
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid AmigoDomainId { get; set; }
        public string Comentario { get; set; }
        public string UrlFoto { get; set; }


        //public string Amigo { get; set; }
        //public Post Posts { get; set; }
        //public List<Post> posts { get; set; }
        public Comment()
        {

        }
        public Comment(Guid AmigoDomainId, string Comentario, string UrlFoto)
        {
            AmigoDomainId = AmigoDomainId;
            Comentario = Comentario;
            UrlFoto = UrlFoto;
        }
    }
}
