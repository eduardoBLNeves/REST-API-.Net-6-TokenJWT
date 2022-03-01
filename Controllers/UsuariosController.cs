using API.Models.Entities;
using API.Models.Entities.Usuarios;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly IUsuariosRepository repo;
        public UsuariosController(IUsuariosRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Cadastra novo usuário
        /// </summary>
        /// <param name="usuario">Cadastro do usuário</param>
        /// <returns>200, 400 ou 500</returns>
        [HttpPost("Cadastro")]
        public IActionResult Post([FromBody] PostUsuario usuario)
        {
            if (!ModelState.IsValid) return BadRequest();

            var retorno = repo.Post(usuario);
            if (retorno.Status == Status.Ok) {
                var response = new UsuarioTokenResponse()
                {
                    token = retorno.Content.ToString()
                };
                return Ok(response); 
            }
            else return BadRequest(retorno.Content.ToString());
        }

        /// <summary>
        /// Get Login
        /// </summary>
        /// <param name="usuario">Login do usuário</param>
        /// <returns>email e Token do usuário</returns>
        [HttpPost("Login")]
        public IActionResult GetLogin([FromBody] GetUsuario usuario)
        {
            if (!ModelState.IsValid) return BadRequest();

            var retorno = repo.GetOne(usuario); 
            if (retorno.Status == Status.Ok)
            {
                var response = new UsuarioTokenResponse()
                {
                    token = retorno.Content.ToString()
                };
                return Ok(response);
            }
            else return BadRequest(retorno.Content.ToString());
        }

        /// <summary>
        /// Retorna todos os usuários
        /// </summary>
        /// <returns>Retorna todos os usuários</returns>
        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(repo.GetAll());
        }

        [Authorize]
        [HttpGet("CheckToken")]
        public IActionResult CheckToken()
        {
            return Ok();
        }

    }
}
