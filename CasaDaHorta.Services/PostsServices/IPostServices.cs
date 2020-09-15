using CasaDaHora.Domain.Post;
using CasaDaHora.Domain.Post.IPostRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHorta.Services.PostsServices
{
    public interface IPostServices
    {
        IPostRepository PostRepository { get; }

        PostCreateResult CriarPost(PostRequest postData);
        IEnumerable<Post> ObterTodosPosts();
    }
}
