using CasaDaHora.Domain.Account;
using CasaDaHora.Domain.Post;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CasaDaHora.Domain.Comment
{
    public class Comments
    {
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }
        public Account.Account Account { get; set; }
        public Post.Post Post { get; set; }
    }
}
