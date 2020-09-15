using CasaDaHora.Domain.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Comment
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Comentario { get; set; }
        public string Amigo { get; set; }

    }
}
