using m3s02_auth.DTO;
using m3s02_auth.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace m3s02_auth.Controllers
{
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoServices _autenticacaoService;

        public AutenticacaoController(IAutenticacaoServices autenticacaoServices)
        {
            _autenticacaoService = autenticacaoServices;
        }

        [HttpPost("logar")]
        [AllowAnonymous]
        public IActionResult Logar(LoginDTO loginDTO)
        {
            if (!_autenticacaoService.Autenticar(loginDTO)) 
                return Unauthorized("Usuario ou Senha inválidos");

            string token = _autenticacaoService.GerarToken(loginDTO);
            return Ok(token);

        }
    }
}
