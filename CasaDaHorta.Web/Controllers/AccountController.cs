using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDaHorta.CrossCutting.FotosPerfil;
using CasaDaHorta.Services.Account;
using CasaDaHorta.Web.Models;
using CasaDaHorta.Web.ViewModel.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CasaDaHorta.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService AccountService { get; set; }
        private IAccountIdentityManager AccountIdentityManager { get; set; }
        public AccountController(IAccountService accountService, 
                                 IAccountIdentityManager accountIdentityManager)
        {
            this.AccountService = accountService;
            this.AccountIdentityManager = accountIdentityManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {

            try
            {

                var result = await this.AccountIdentityManager.Login(model.Email, model.Password);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Login ou senha inválidos");
                    return View(model);
                }

                var client = new RestClient();
                var request = new RestRequest("https://localhost:44300/api/authenticate/token", DataFormat.Json);
                request.AddJsonBody(model);
                var response = client.Post<string>(request);
                HttpContext.Session.SetString("Token", response.Data);

                if (!String.IsNullOrWhiteSpace(returnUrl))
                    return Redirect(returnUrl);

                return Redirect("/");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View(model);
            }

        }
        public IActionResult Register()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await this.AccountService.Register(model.Nome, model.DataNascimento, model.Email, model.Password);
                    return Redirect("/");
                }
                return View(model);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            this.AccountIdentityManager.Logout();
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }
            return Redirect("/Account/Login");
        }

    }
}