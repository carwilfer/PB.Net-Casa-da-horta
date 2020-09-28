using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CasaDaHora.Domain.Account.Repository;
using CasaDaHora.Domain.Comment;
using CasaDaHora.Domain.Post;
using CasaDaHorta.Repository.Context;
using CasaDaHorta.Web.ViewModel.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using RestSharp;

namespace CasaDaHorta.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        private readonly CasaDaHortaContext _casaDaHortaContext;

        public PostController(IAccountRepository accountRepository, CasaDaHortaContext redeDoVerdeContext)
        {
            _accountRepository = accountRepository;
            _casaDaHortaContext = redeDoVerdeContext;
        }

        // GET: PostController
        public ActionResult Index()
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44300/api/posts", DataFormat.Json);
            var response = client.Get<List<Post>>(request.AddHeader("Authorization", "Bearer " + LimpandoToken(key)));

            return View(response.Data);
        }

        // GET: PostController/Details/5
        public ActionResult Details(Guid id)
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44300/api/posts/" + id, DataFormat.Json);
            var response = client.Get<PostResponse>(request.AddHeader("Authorization", "Bearer " + LimpandoToken(key)));

            var requestComments = new RestRequest("https://localhost:44300/api/comments/", DataFormat.Json);
            var responseComments = client.Get<List<Comments>>(requestComments.AddHeader("Authorization", "Bearer " + LimpandoToken(key)));

            List<Comments> listaComments = new List<Comments>();

            foreach (var item in responseComments.Data)
            {
                foreach (var item5 in response.Data.Comments)
                {
                    if (item.Id == item5.Id)
                    {
                        listaComments.Add(item);
                    }
                }
            }

            ViewBag.Comments = listaComments;

            return View(response.Data);
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = new RestClient();
                    var request = new RestRequest("https://localhost:44300/api/posts", DataFormat.Json);
                    var key = HttpContext.Session.GetString("Token");

                    string value = LimpandoToken(key);
                    var jwt = value;
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(jwt);

                    CancellationToken cancellationToken;

                    var account = _accountRepository.FindByIdAsync(token.Subject, cancellationToken);

                    model.ImagePost = UploadFotoPost(model.Foto).Result;

                    model.Account = account.Result;

                    request.AddJsonBody(model);
                    var response = client.Post<Post>(request.AddHeader("Authorization", "Bearer " + LimpandoToken(key)));

                    return Redirect("/");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44300/api/posts/" + id, DataFormat.Json);
            var response = client.Get<PostViewModels>(request.AddHeader("Authorization", "Bearer " + LimpandoToken(key)));

            return View(response.Data);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, PostViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = new RestClient();
                    var request = new RestRequest("https://localhost:44300/api/posts/" + id, DataFormat.Json);

                    model.ImagePost = UploadFotoPost(model.Foto).Result;

                    request.AddJsonBody(model);
                    var key = HttpContext.Session.GetString("Token");

                    var response = client.Put<Post>(request.AddHeader("Authorization", "Bearer " + LimpandoToken(key)));

                    return Redirect("/");
                }

                return Redirect("/");
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44300/api/posts/" + id, DataFormat.Json);
            var response = client.Get<Post>(request.AddHeader("Authorization", "Bearer " + LimpandoToken(key)));

            return View(response.Data);
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
            try
            {
                var client = new RestClient();

                var key = HttpContext.Session.GetString("Token");
                var request = new RestRequest("https://localhost:44300/api/posts/" + id, DataFormat.Json);
                var response = client.Get<Post>(request.AddHeader("Authorization", "Bearer " + LimpandoToken(key)));

                post = response.Data;

                using (var transaction = _casaDaHortaContext.Database.BeginTransaction())
                {
                    try
                    {

                        foreach (var item in post.Comments)
                        {
                            var requestComments = new RestRequest("https://localhost:44300/api/comments/" + item.Id, DataFormat.Json);

                            var responseComments = client.Delete<Comments>(requestComments.AddHeader("Authorization", "Bearer " + LimpandoToken(key)));
                        }

                        request = new RestRequest("https://localhost:44300/api/posts/" + id, DataFormat.Json);

                        response = client.Delete<Post>(request.AddHeader("Authorization", "Bearer " + LimpandoToken(key)));

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
                return Redirect("/");
            }
            catch
            {
                return View();
            }
        }

        public string LimpandoToken(string key)
        {
            string[] RetiraDoisPontos = key.Split(":");
            string[] RetiradoispontosEBarras = RetiraDoisPontos[1].Split("\\");
            string[] RetiradoispontosEBarrasEChaves = RetiradoispontosEBarras[0].Split("}");
            string[] RetiradoispontosEBarrasEChavesEChaves = RetiradoispontosEBarrasEChaves[0].Split('"');

            return RetiradoispontosEBarrasEChavesEChaves[1];
        }



        private async Task<string> UploadFotoPost(IFormFile foto)
        {
            var reader = foto.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(@"");
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("fotos-post");
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            await blob.UploadFromStreamAsync(reader);
            var destinoDaImagemNaNuvem = blob.Uri.ToString();
            return destinoDaImagemNaNuvem;
        }
    }
}
