using CasaDaHora.Domain.Post;
using CasaDaHora.Domain.Post.IPostRepository;
using CasaDaHorta.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Repository.AccountRepository
{
    public class PostRepository : IPostRepository
    {
        private bool disposedValue;
        private CasaDaHortaContext Context { get; set; }
        public PostRepository(CasaDaHortaContext casaDaHortaContext)
        {
            this.Context = casaDaHortaContext;
        }

        public async Task CreatePost(Postagem post)
        {
            try
            {
                await Context.Postagens.AddAsync(post);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Postagem> GetById(Guid id)
        {
            return await Context.Postagens.FirstOrDefaultAsync(post => post.Id == id);
        }

        public async Task<List<Postagem>> GetAll()
        {
            return await Context.Postagens.ToListAsync();
        }

        public async Task RemovePost(Postagem post)
        {
            try
            {
                Context.Postagens.Remove(post);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdatePost(Postagem post)
        {
            try
            {
                Context.Postagens.Update(post);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
