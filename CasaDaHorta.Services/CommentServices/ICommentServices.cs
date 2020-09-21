using CasaDaHora.Domain.Comment;
using CasaDaHora.Domain.Comment.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using static CasaDaHorta.Services.CommentServices.CommentServices;

namespace CasaDaHorta.Services.CommentServices
{
    public interface ICommentServices
    {
        ICommentRepository CommentRepository { get; }

        CommentCreateResult CriarComment(CommentRequest postData);
        IEnumerable<Comentario> ObterTodosComment();
    }
}
