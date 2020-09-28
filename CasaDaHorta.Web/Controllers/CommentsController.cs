﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CasaDaHora.Domain.Account.Repository;
using CasaDaHora.Domain.Comment;
using CasaDaHora.Domain.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CasaDaHorta.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public CommentsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        // GET: CommentsController
        public ActionResult Index()
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44300/api/comments", DataFormat.Json);
            var response = client.Get<List<Comments>>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            return View(response.Data);
        }

        // GET: CommentsController/Details/5
        public ActionResult Details(Guid id)
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44300/api/comments/" + id, DataFormat.Json);
            var response = client.Get<Comments>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            return View(response.Data);
        }

        // GET: CommentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comments model, [FromRoute] Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = new RestClient();
                    var requestPost = new RestRequest("https://localhost:44300/api/posts/" + id, DataFormat.Json);
                    var key = HttpContext.Session.GetString("Token");

                    var responsePost = client.Get<Post>(requestPost.AddHeader("Authorization", "Bearer " + KeyValue(key)));

                    string value = KeyValue(key);
                    var jwt = value;
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(jwt);

                    CancellationToken cancellationToken;

                    var account = _accountRepository.FindByIdAsync(token.Subject, cancellationToken);

                    model.Account = account.Result;
                    model.Post = responsePost.Data;

                    var request = new RestRequest("https://localhost:44300/api/comments", DataFormat.Json);

                    request.AddJsonBody(model);
                    var response = client.Post<Comments>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

                    return Redirect("/");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentsController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44300/api/comments/" + id, DataFormat.Json);
            var response = client.Get<Comments>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            return View(response.Data);
        }

        // POST: CommentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Comments model)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest("https://localhost:44300/api/comments/" + id, DataFormat.Json);
                request.AddJsonBody(model);
                var key = HttpContext.Session.GetString("Token");

                var response = client.Put<Comments>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

                return Redirect("/");
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentsController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44300/api/comments/" + id, DataFormat.Json);
            var response = client.Get<Comments>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            return View(response.Data);
        }

        // POST: CommentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest("https://localhost:44300/api/comments/" + id, DataFormat.Json);
                var key = HttpContext.Session.GetString("Token");

                var response = client.Delete<Comments>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

                return Redirect("/");
            }
            catch
            {
                return View();
            }
        }

        public string KeyValue(string key)
        {
            string[] peloAmorDeDeusFunciona = key.Split(":");
            string[] agrVai = peloAmorDeDeusFunciona[1].Split("\\");
            string[] agrVaiPF = agrVai[0].Split("}");
            string[] agrVaiPF2 = agrVaiPF[0].Split('"');

            return agrVaiPF2[1];
        }
    }
}
