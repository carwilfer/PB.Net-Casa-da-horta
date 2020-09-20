using CasaDaHora.Domain.Post;
using CasaDaHora.Domain.Post.IPostRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHorta.Services.PostsServices
{
    public class PostServices : IPostServices
    {
        public PostServices(IPostRepository postRepository)
        {
            PostRepository = postRepository;
        }

        public IPostRepository PostRepository { get; set;  }

        public PostCreateResult CriarPost(PostRequest postData)
        {
            var result = new PostCreateResult();

            if (string.IsNullOrEmpty(postData.Texto))
                result.Erros.Add("Texto do post é obrigatório");

            if (!result.Sucesso)
                return result;

            var post = new Post
            {
                Texto = postData.Texto,
                UrlFoto = postData.UrlImagem
            };

            PostRepository.Salvar(post);

            return result;
        }

        public IEnumerable<Post> ObterTodosPosts()
        {
            return PostRepository.ObterTodos();
        }
    }

    public class PostCreateResult
    {
        public bool Sucesso => Erros.Count == 0;
        public List<string> Erros { get; } = new List<string>();
    }
}
