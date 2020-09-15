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

                var result = await this.AccountIdentityManager.Login(model.UserName, model.Password);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Login ou senha inválidos");
                    return View(model);
                }

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
        // GET: Perfils/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: Perfils/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Perfils/Create
        //[HttpPost]
        //public async Task<ActionResult> Create(PerfilInputModel model, IFormFile foto)
        //{
        //    try
        //    {
        //        var uri = await ArmazenamentoDeFotos.ArmazenarFotoDePerfil(foto);

        //        model.UrlFoto = uri.AbsoluteUri;

        //        await CasaDaHorta.Api.Post("perfil", model);

        //        return RedirectToAction("index", "home");
        //    }
        //    catch (Exception)
        //    {
        //        return base.ValidationProblem();
        //    }
        //}

        // GET: Perfils/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Perfils/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Perfils/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Perfils/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}