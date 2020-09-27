using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Comment.Repository
{
    public class CommentsResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Account.AccountResponse Account { get; set; }
        public Post.PostViewModel Post { get; set; }
    }
}
