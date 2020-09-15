using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHora.Domain.Comment.Repository
{
    public interface ICommentRepository
    {
        Task<Comment> Comments(string comentario, string UserName);

        void Salvar(Comment comment);
        IEnumerable<Comment> ObterTodos();
    }
}
