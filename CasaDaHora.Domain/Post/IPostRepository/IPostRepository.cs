using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHora.Domain.Post.IPostRepository
{
    public interface IPostRepository
    {
        void Salvar(Post post);
        Task<Post> GetById(Guid id);
        Task<bool> UpdatePost(Guid id, string UrlFoto, string Comentario);
        IEnumerable<Post> ObterTodos();
        Task<Post> CreatePost(Guid Id, Guid AmigoDomainId, string UrlFoto, string Comentario);
        Task<bool> RemovePost(Guid id);


        //Task<Post> GetAccountByUserNamePassword(string userName, string password);
    }
}
