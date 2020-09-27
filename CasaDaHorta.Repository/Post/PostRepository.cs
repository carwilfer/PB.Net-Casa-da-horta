using CasaDaHora.Domain.Comment;
using CasaDaHora.Domain.Post;
using CasaDaHorta.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Repository.AccountRepository
{
    public class PostRepository
    {
        private readonly CasaDaHortaContext casaDaHortaContext;
        public PostRepository(CasaDaHortaContext casaDaHortaContext)
        {
            this.casaDaHortaContext = casaDaHortaContext;
        }
               
    }
}
