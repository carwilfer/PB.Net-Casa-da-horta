using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;

namespace CasaDaHora.Domain.Post
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string ImagePost { get; set; }
        public Account.AccountResponse Account { get; set; }
        public List<Comment.Comments> Comments { get; set; }
    }
}
