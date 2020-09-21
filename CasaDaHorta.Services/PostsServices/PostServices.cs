using CasaDaHora.Domain.Post;
using CasaDaHora.Domain.Post.IPostRepository;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Services.PostsServices
{
    public class PostServices : IPostServices
    {
        public IPostRepository PostRepository { get; set; }
        public PostServices(IPostRepository postRepository)
        {
            PostRepository = postRepository;
        }

        public async Task<Postagem> CreatePost(Guid Id, Guid amigoDomainId, string urlFoto, string textoFoto)
        {
            Postagem postagem = new Postagem(Id, amigoDomainId, urlFoto, textoFoto);
            await PostRepository.CreatePost(postagem);
            return postagem;

        }

        public async Task<bool> UpdatePost(Guid Id, string UrlFoto, string textoFoto)
        {
            try
            {
                var post = await PostRepository.GetById(Id);

                if (!String.IsNullOrEmpty(UrlFoto))
                {
                    post.UrlFoto = UrlFoto;
                }
                if (!String.IsNullOrEmpty(textoFoto))
                {
                    post.TextoFoto = textoFoto;
                }

                await PostRepository.UpdatePost(post);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<Postagem> GetById(Guid id)
        {
            return await PostRepository.GetById(id);
        }

        public async Task<bool> RemovePost(Guid Id)
        {
            try
            {
                var post = await PostRepository.GetById(Id);

                await PostRepository.RemovePost(post);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<Postagem> GetAll()
        {
            return await PostRepository.GetAll();

        }

        public IEnumerable<Postagem> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Postagem> ObterTodosPosts()
        {
            throw new NotImplementedException();
        }

        public void Salvar(Comentario comentario)
        {
            throw new NotImplementedException();
        }
    }
}
