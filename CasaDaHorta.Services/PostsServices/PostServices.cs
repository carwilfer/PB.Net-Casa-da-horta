using CasaDaHora.Domain.Post;
using CasaDaHora.Domain.Post.IPostRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Services.PostsServices
{
    public class PostServices : IPostServices
    {
        public PostServices(IPostRepository postRepository)
        {
            PostRepository = postRepository;
        }

        public IPostRepository PostRepository { get; set;  }

        public Task<Post> CreatePost(Guid Id, Guid AmigoDomainId, string UrlFoto, string Comentario)
        {
            throw new NotImplementedException();
        }

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

        public Task<Post> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> ObterTodosPosts()
        {
            return PostRepository.ObterTodos();
        }

        public Task<bool> RemovePost(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePost(Guid Id, string UrlFoto, string Comentario)
        {
            throw new NotImplementedException();
        }
    }

    public class PostCreateResult
    {
        public bool Sucesso => Erros.Count == 0;
        public List<string> Erros { get; } = new List<string>();
    }
}
