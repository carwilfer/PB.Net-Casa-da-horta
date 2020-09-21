using CasaDaHora.Domain.Comment;
using CasaDaHora.Domain.Comment.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHorta.Services.CommentServices
{
    public class CommentServices : ICommentServices
    {
       
        private ICommentRepository CommentRepository { get; set; }

        ICommentRepository ICommentServices.CommentRepository => throw new NotImplementedException();

        public CommentCreateResult CriarComment(CommentRequest commentData)
        {
            var result = new CommentCreateResult();

            if (string.IsNullOrEmpty(commentData.Texto))
                result.Erros.Add("Texto do post é obrigatório");

            if (!result.Sucesso)
                return result;

            var comentarios = new Comentario
            {
                Comentario = commentData.Texto,
                Amigo = commentData.UrlImagem
            };

            CommentRepository.Salvar(comentarios);

            return result;
        }

        public IEnumerable<Comentario> ObterTodosComment()
        {
            return CommentRepository.ObterTodos();
        }

        CommentCreateResult ICommentServices.CriarComment(CommentRequest postData)
        {
            throw new NotImplementedException();
        }

        public class CommentCreateResult
        {
            public bool Sucesso => Erros.Count == 0;
            public List<string> Erros { get; } = new List<string>();
        }
    }
}
