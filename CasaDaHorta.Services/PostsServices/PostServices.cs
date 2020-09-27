using CasaDaHora.Domain.Account.Repository;
using CasaDaHora.Domain.Post;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Services.PostsServices
{
    public class PostServices : IPostServices
    {
        private readonly IAccountRepository _accountRepository;

        public PostServices(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
    }
}
