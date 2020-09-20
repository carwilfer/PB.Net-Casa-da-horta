using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDaHorta.Services.PostsServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CasaDaHorta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public IPostServices PostServices { get; set; }
       
        public PostController(IPostServices postServices)
        {
            PostServices = postServices;
        }

        // GET: api/<PostController>
        [HttpGet]
        public IActionResult Get()
        {
            var posts = PostServices.ObterTodosPosts();

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostRequest request)
        {
            var result = PostServices.CriarPost(request);

            if (result.Sucesso)
                return Ok();
            else
                return UnprocessableEntity(result.Erros);
        }


        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
