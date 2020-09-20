using CasaDaHora.Domain.Post;
using CasaDaHora.Domain.Post.IPostRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Services.PostsServices
{
    public interface IPostServices
    {
        IPostRepository PostRepository { get; set; }
        Task<Post> CreatePost(Guid Id, Guid AmigoDomainId, string UrlFoto, string Comentario );
        Task<bool> UpdatePost(Guid Id, string UrlFoto, string Comentario);
        Task<Post> GetById(Guid  id);
        PostCreateResult CriarPost(PostRequest postData);
        Task<bool> RemovePost(Guid Id);
        IEnumerable<Post> ObterTodosPosts();
    }
}
