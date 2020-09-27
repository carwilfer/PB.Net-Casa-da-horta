using CasaDaHora.Domain.Account.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Services.Account
{
    public class AccountService : IAccountService
    {
        private IAccountRepository AccountRepository { get; set; }
        public AccountService(IAccountRepository accountRepository)
        {
            this.AccountRepository = accountRepository;
        }

        public async Task Register(string nome, DateTime dataNascimento, string email, string password)
        {
            await AccountRepository.CreateUser(nome, dataNascimento, email, password);
        }
    }

}

