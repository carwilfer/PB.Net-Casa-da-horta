using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHorta.Services.PostsServices
{
    public class PostRequest
    {
        public string Texto { get; set; }
        public string UrlImagem { get; set; }
        public string Proprietario { get; set; }
    }
}
