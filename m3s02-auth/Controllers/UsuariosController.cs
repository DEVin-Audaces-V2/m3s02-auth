using m3s02_auth.DTO;
using m3s02_auth.Interfaces.Services;
using m3s02_auth.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace m3s02_auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        public UsuariosController(IConfiguration configuration,
                                   IUsuarioService usuarioService) : base(configuration)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<UsuarioGetDTO> Post(UsuarioDTO usuario)
        {
            var usuarioDB = _usuarioService.Criar(new Usuario(usuario));


            return Created(Request.PathBase, new UsuarioGetDTO(usuarioDB));
        }
        [HttpPut("{login}")]
        [Authorize(Roles ="Professor")]
        public ActionResult<UsuarioGetDTO> Put(UsuarioDTO usuario, string login)
        {
            usuario.Login = login;
            var usuarioDB = _usuarioService.Atualizar(new Usuario(usuario));


            return Ok(new UsuarioGetDTO(usuarioDB));
        }
        [HttpGet]
        [Authorize(Roles = "Professor,Aluno")]
        public ActionResult<List<UsuarioGetDTO>> Get()
        {
            var usuarios = _usuarioService.Obter();


            return Ok(usuarios.Select(x => new UsuarioGetDTO(x)));
        }
        [HttpGet("{login}")]
        [Authorize(Roles = "Professor,Aluno")]
        public ActionResult<List<UsuarioGetDTO>> Get(string login)
        {
            var usuarios = _usuarioService.ObterPorId(login);


            return Ok( new UsuarioGetDTO(usuarios));
        }
        [HttpDelete("{login}")]
        [Authorize(Roles = "Professor")]
        public ActionResult<List<UsuarioGetDTO>> Deletar(string login)
        {
            _usuarioService.Deletar(login);
            return NoContent();
        }
    }
}
