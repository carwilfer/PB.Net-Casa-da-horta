using CasaDaHora.Domain.Account.Repository;
using CasaDaHora.Domain.Account;
using Microsoft.AspNetCore.Identity;
using CasaDaHora.Domain.Account;
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
        private SignInManager<Accounty> SignInManager { get; set; }

        public AccountIdentityManager(IAccountRepository accountRepository, SignInManager<Accounty> signInManager)
        {
            this.Repository = accountRepository;
            this.SignInManager = signInManager;

        }

        public async Task<SignInResult> Login(string userName, string password)
        {
            var account = await this.Repository.GetAccountByUserNamePassword(userName, password);

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
