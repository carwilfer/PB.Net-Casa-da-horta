using CasaDaHora.Domain.Account;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;


namespace CasaDaHorta.Web.ViewModel.Post
{
    public class PostViewModels
    {
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }
        public IFormFile Foto { get; set; }
        public string ImagePost { get; set; }

        public CasaDaHora.Domain.Account.Account Account { get; set; }

        [JsonIgnore]
        public List<CasaDaHora.Domain.Comment.Comments> Comments { get; set; }
    }
}
