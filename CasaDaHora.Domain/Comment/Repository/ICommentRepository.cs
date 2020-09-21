using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHora.Domain.Comment.Repository
{
    public interface ICommentRepository
    {
        Task<Comentario> Comments(string comentario, string UserName);
        void Salvar(Comentario comment);
        IEnumerable<Comentario> ObterTodos();
    }
}
