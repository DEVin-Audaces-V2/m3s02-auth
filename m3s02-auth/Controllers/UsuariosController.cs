using m3s02_auth.DTO;
using m3s02_auth.Interfaces.Services;
using m3s02_auth.Model;
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
        public ActionResult<UsuarioGetDTO> Post(UsuarioDTO usuario)
        {
            var usuarioDB = _usuarioService.Criar(new Usuario(usuario));


            return Created(Request.PathBase, new UsuarioGetDTO(usuarioDB));
        }
        [HttpPut("{login}")]
        public ActionResult<UsuarioGetDTO> Put(UsuarioDTO usuario, string login)
        {
            usuario.Login = login;
            var usuarioDB = _usuarioService.Atualizar(new Usuario(usuario));


            return Ok(new UsuarioGetDTO(usuarioDB));
        }
        [HttpGet]
        public ActionResult<List<UsuarioGetDTO>> Get()
        {
            var usuarios = _usuarioService.Obter();


            return Ok(usuarios.Select(x => new UsuarioGetDTO(x)));
        }
        [HttpGet("{login}")]
        public ActionResult<List<UsuarioGetDTO>> Get(string login)
        {
            var usuarios = _usuarioService.ObterPorId(login);


            return Ok( new UsuarioGetDTO(usuarios));
        }
        [HttpDelete("{login}")]
        public ActionResult<List<UsuarioGetDTO>> Deletar(string login)
        {
            _usuarioService.Deletar(login);
            return NoContent();
        }
    }
}
