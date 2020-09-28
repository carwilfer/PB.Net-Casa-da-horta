using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CasaDaHorta.CrossCutting.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CasaDaHorta.Web.Controllers
{
    public class UploadController : Controller
    {
        private AzureStorage AzureStorage { get; set; }

        public UploadController(AzureStorage azureStorage)
        {
            AzureStorage = azureStorage;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] IFormFile file)
        {
            var client = new RestClient();
            var request = new RestRequest("https://localhost:44300/api/posts", DataFormat.Json);
            var key = HttpContext.Session.GetString("Token");

            string value = KeyValue(key);
            var jwt = value;
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            var ms = new MemoryStream();

            //para impedir que seja qualquer tipo arquivo
            if (file.ContentType.ToLower() != "image/jpg" && file.ContentType.ToLower() 
                                           != "image/jpeg" && file.ContentType.ToLower() 
                                           != "image/png" && file.ContentType.ToLower() != "image/bmp")
            {
                ModelState.AddModelError("Image", "Image deve ser com a extensão .jpg, .png ou bmp");
                return View();
            }

            using (var fileUpload = file.OpenReadStream())
            {
                await fileUpload.CopyToAsync(ms);
                fileUpload.Close();
            }

            var urlAzure = await this.AzureStorage.SaveToStorage(ms.ToArray(), $"{Guid.NewGuid().ToString().Replace("-", "")}.jpg");

            ViewBag.UrlGerada = urlAzure;

            return View();
        }
        public string KeyValue(string key)
        {
            string[] RetiraDoisPontos = key.Split(":");
            string[] RetiradoispontosEBarras = RetiraDoisPontos[1].Split("\\");
            string[] RetiradoispontosEBarrasEChaves = RetiradoispontosEBarras[0].Split("}");
            string[] RetiradoispontosEBarrasEChavesEChaves = RetiradoispontosEBarrasEChaves[0].Split('"');

            return RetiradoispontosEBarrasEChavesEChaves[1];
        }
    }
}
