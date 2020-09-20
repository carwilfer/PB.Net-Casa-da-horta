using CasaDaHora.Domain.Account;
using CasaDaHora.Domain.Amigo;
using CasaDaHora.Domain.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Post
{
    public class Post
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public string UrlFoto { get; set; }
        public AmigoDomain Amigo { get; set; }
        public Accounty Accounty { get; set; }

        //public List<Comment> Comments { get; set; }
        //public Comment Comments { get; set; }
    }
}
