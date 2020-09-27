using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CasaDaHora.Domain.Account
{
    public class AccountResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        [JsonIgnore]
        public List<Post.Post> Posts { get; set; }

        [JsonIgnore]
        public List<Comment.Comments> Comments { get; set; }
    }
}
