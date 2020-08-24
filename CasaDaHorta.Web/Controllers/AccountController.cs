using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDaHorta.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace CasaDaHorta.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService AccountService { get; set; }
        private IAccountIdentityManager AccountIdentityManager { get; set; }
        public AccountController(IAccountService accountService, IAccountIdentityManager accountIdentityManager)
        {
            this.AccountService = accountService;
            this.AccountIdentityManager = accountIdentityManager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
