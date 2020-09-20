using CasaDaHora.Domain.Amigo.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHorta.Services.AmigoServices
{
    public interface IAmigoServices
    {
        IAmigoRepository AmigoRepository { get; set; }

        Tarefa<UserResponse> CreateUser(id do Guid  , string firstName, string lastName, string email, string senha, string photoUrl, status bool );

        Tarefa<Usuário> GetUserByEmail(string e-mail );

        Tarefa<UserResponse> GetByEmail(string e-mail );

        Tarefa<usuário> GetUserById(id do Guid  );

        Tarefa<Resposta do usuário> GetById(id do Guid  );

        Tarefa<List<UserResponse>> GetAll();

        Tarefa<bool> UpdateUser(id do Guid  , string firstName, string lastName, string email, string senha, string photoUrl);

        Tarefa<bool> RemoveUser(usuário usuário);

        Tarefa<List<UserFollowersResponse>> GetFollowers(Guid userId);

        Task<bool> AddFollower(Guid userId, Guid followerId);

        Task<bool> RemoveFollower(Guid userId, Guid followerId);
    }
}
