using CasaDaHora.Domain.Comment;
using CasaDaHora.Domain.Comment.Repository;
using CasaDaHorta.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Repository.AccountRepository
{
    public class CommentRepository : ICommentRepository
    {
        public CasaDaHortaContext Db { get; }
        public Task<Comentario> Comments(string comentario, string UserName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comentario> ObterTodos()
        {
            return Db.Comment;
        }

        public void Salvar(Comentario comment)
        {
            Db.Comment.Add(comment);
            Db.SaveChanges();
        }
    }
}
