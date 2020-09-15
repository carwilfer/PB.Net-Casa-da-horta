using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDaHorta.Web.Models.PostModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public string UrlImagem { get; set; }
        public string Proprietario { get; set; }
        public List<string> Comentarios { get; set; } = new List<string>();
    }
}
