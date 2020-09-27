using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHora.Domain.Comment.Repository
{
    public interface ICommentRepository
    {
        Task<Comments> Comments(string comentario, string UserName);
        void Salvar(Comments comment);
        IEnumerable<Comments> ObterTodos();
    }
}
