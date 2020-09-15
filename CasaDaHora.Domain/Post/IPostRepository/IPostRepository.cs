using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHora.Domain.Post.IPostRepository
{
    public interface IPostRepository
    {
        void Salvar(Post post);
        IEnumerable<Post> ObterTodos();

        //Task<Post> GetAccountByUserNamePassword(string userName, string password);
    }
}
