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

        IEnumerable<Postagem> ObterTodosPosts();
        void Salvar(Comentario comentariom);
        Task<Postagem> CreatePost(Guid Id, Guid amigoDomainId, string urlFoto, string textoFoto);
        Task<bool> UpdatePost(Guid Id, string UrlFoto, string textoFoto);
        Task<Postagem> GetById(Guid id);
        Task<Postagem> GetAll();
        IEnumerable<Postagem> GetAll(Guid id);
        Task<bool> RemovePost(Guid Id);
    }
}
