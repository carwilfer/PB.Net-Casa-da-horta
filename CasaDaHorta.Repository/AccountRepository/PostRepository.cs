using CasaDaHora.Domain.Post;
using CasaDaHora.Domain.Post.IPostRepository;
using CasaDaHorta.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHorta.Repository.AccountRepository
{
    public class PostRepository : IPostRepository
    {
        public PostRepository(CasaDaHortaContext Db)
        {
            this.Db = Db;
        }

        public CasaDaHortaContext Db { get; }

        public IEnumerable<Post> ObterTodos()
        {
            return Db.Post;
        }

        public void Salvar(Post post)
        {
            Db.Post.Add(post);
            Db.SaveChanges();
        }
    }
}
