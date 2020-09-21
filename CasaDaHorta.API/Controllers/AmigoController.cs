using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDaHora.Domain.Amigo;
using CasaDaHora.Domain.Amigo.Repository;
using CasaDaHorta.Services.AmigoServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CasaDaHorta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmigoController : ControllerBase
    {
        public IAmigoServices AmigoServices { get; set; }
        public AmigoController(IAmigoServices iAmigoServices)
        {
            AmigoServices = iAmigoServices;
        }

        public AmigoController(AmigoServices amigoServices)
        {
            this.AmigoServices = amigoServices;

        }


        //[Route("Token")]
        //[HttpPost]
        //[RequireHttps]
        //public IActionResult Token([FromBody] LoginRequest loginRequest)
        //{
        //    var token = this.AmigoServices.Login(loginRequest.Login, loginRequest.Senha);

        //    if (String.IsNullOrWhiteSpace(token))
        //    {
        //        return BadRequest("Login ou senha invalida");
        //    }

        //    return Ok(new
        //    {
        //        Token = token
        //    });
        //}
        //public class LoginRequest
        //{
        //    public String Login { get; set; }
        //    public String Senha { get; set; }

        //}




        // GET: api/<AmigoController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await AmigoServices.GetAll());
        }

        // GET api/<AmigoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await AmigoServices.GetById(id));
        }

        // POST api/<AmigoController>
        [HttpPost]
        public void Post([FromBody] AmigoDomain amigoDomain)
        {
            try
            {
                this.AmigoServices.SaveAmigoDomain(amigoDomain);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<AmigoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] string Nome, string Sobrenome, string Email, string Password, string UrlFoto)
        {
            AmigoDomain amigoDomainEncontrado = await AmigoServices.GetAmigoDomainById(id);
            AmigoDomain novoAmigoDomain = new AmigoDomain();

            novoAmigoDomain.Id = id;

            if (string.IsNullOrEmpty(Nome))
                novoAmigoDomain.Nome = amigoDomainEncontrado.Nome;
            if (string.IsNullOrEmpty(Sobrenome))
                novoAmigoDomain.Sobrenome = amigoDomainEncontrado.Sobrenome;
            if (string.IsNullOrEmpty(Email))
                novoAmigoDomain.Email = amigoDomainEncontrado.Email;
            if (string.IsNullOrEmpty(Password))
                novoAmigoDomain.Password = amigoDomainEncontrado.Password;
            if (string.IsNullOrEmpty(UrlFoto))
                novoAmigoDomain.UrlFoto = amigoDomainEncontrado.UrlFoto;

            bool result = await AmigoServices.UpdateAmigoDomain(id, Nome, Sobrenome, Email, Password, UrlFoto);

            if (!result)
                return BadRequest();

            return Ok();
        }

        // DELETE api/<AmigoController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            this.AmigoServices.Delete(id);

            //AmigoDomain amigoDomain = await AmigoServices.GetAmigoDomainById(id);

            //bool result = await AmigoServices.ExcluirRemover(id);

            //if (!result)
            //    return BadRequest();

            //return Ok();
        }
        [HttpGet]
        [Route("{id}/followers")]
        public IActionResult GetSeguidores([FromRoute] Guid id)
        {
            return Ok(AmigoServices.GetSeguidores(id));
        }
        [HttpPut]
        [Route("{id}/followers")]
        public async Task<IActionResult> AddFollower([FromRoute] Guid id, [FromBody] SeguidorRequest seguidor)
        {
            bool result = await AmigoServices.AddSeguidores(id, seguidor.Id);

            if (!result)
                return BadRequest();

            return Ok();
        }

        //[HttpDelete]
        //[Route("{id}/followers")]
        //public async Task<bool> RemoveFollower([FromRoute] Guid id, [FromBody] SeguidorRequest seguidor)
        //{
        //    bool result = await AmigoServices.ExcluirOk(id, seguidor.Id);

        //    if (!result)
        //        return BadRequest();

        //    return Ok();
        //}
    }
}
