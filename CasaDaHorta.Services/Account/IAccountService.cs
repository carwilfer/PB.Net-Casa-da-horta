using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Services.Account
{
    public interface IAccountService
    {
        Task Register(string nome, DateTime dataNascimento, string email, string password);
    }
}
