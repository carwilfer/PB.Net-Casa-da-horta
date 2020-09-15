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
        public string Amigo { get; set; }
    }
}
