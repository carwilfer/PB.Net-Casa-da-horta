using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Services.Account
{
    public interface IAccountIdentityManager
    {
        Task<SignInResult> Login(string userName, string password);
    }
}
