using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHora.Domain.Post.IPostRepository
{
    public interface IPostRepository 
    {
        Task<Postagem> GetById(Guid id);
        Task<List<Postagem>> GetAll();
        Task CreatePost(Postagem post);

        Task UpdatePost(Postagem post);

        Task RemovePost(Postagem post);



        //Task<Post> GetAccountByUserNamePassword(string userName, string password);
    }
}
