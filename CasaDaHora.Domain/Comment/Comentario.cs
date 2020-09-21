using CasaDaHora.Domain.Account;
using CasaDaHora.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Comment
{
    public class Comentario
    {
        public Guid Id { get; set; }
        public Guid AmigoDomainId { get; set; }
        public string Texto { get; set; }
        public string UrlFoto { get; set; }


        //public string Amigo { get; set; }
        //public Post Posts { get; set; }
        //public List<Post> posts { get; set; }
        public Comentario()
        {

        }
        public Comentario(Guid AmigoDomainId, string Comentario, string UrlFoto)
        {
            AmigoDomainId = AmigoDomainId;
            Comentario = Comentario;
            UrlFoto = UrlFoto;
        }
    }
}
