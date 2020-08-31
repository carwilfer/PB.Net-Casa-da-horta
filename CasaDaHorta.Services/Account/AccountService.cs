using CasaDaHora.Domain.Account.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHorta.Services.Account
{
    public class AccountService : IAccountService
    {
        private IAccountRepository AccountRepository { get; set; }
        public AccountService(IAccountRepository accountRepository)
        {
            this.AccountRepository = accountRepository;
        }
    }

}

