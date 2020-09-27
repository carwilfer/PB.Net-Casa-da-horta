using CasaDaHora.Domain.Account.Repository;
using CasaDaHora.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest;

namespace CasaDaHorta.Services.Account
{
    public class AccountIdentityManager : IAccountIdentityManager
    {
        private IAccountRepository Repository { get; set; }
        private SignInManager<CasaDaHora.Domain.Account.Account> SignInManager { get; set; }

        public AccountIdentityManager(IAccountRepository accountRepository, SignInManager<CasaDaHora.Domain.Account.Account> signInManager)
        {
            this.Repository = accountRepository;
            this.SignInManager = signInManager;

        }

        public async Task<SignInResult> Login(string email, string password)
        {
            var account = await this.Repository.GetAccountByEmailPassword(email, password);

            if (account == null)
            {
                return SignInResult.Failed;
            }

            await SignInManager.SignInAsync(account, false);

            return SignInResult.Success;

        }

        public async Task Logout()
        {
            await this.SignInManager.SignOutAsync();
        }

    }
}
