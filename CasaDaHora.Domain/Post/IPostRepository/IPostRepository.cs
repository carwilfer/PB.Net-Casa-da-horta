using CasaDaHora.Domain.Comment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHora.Domain.Post.IPostRepository
{
    public interface IPostRepository 
    {
        Task<Postagem> GetById(Guid id);
        Task CreatePost(Postagem post);
        void Salvar(Comentario comentario);
        Task UpdatePost(Postagem post);

        Task RemovePost(Postagem post);
        Task<Postagem> GetAll();



        //Task<Post> GetAccountByUserNamePassword(string userName, string password);
    }
}
