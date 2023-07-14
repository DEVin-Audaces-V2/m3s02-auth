using m3s02_auth.DTO;
using m3s02_auth.Exceptions;
using m3s02_auth.Interfaces.Services;
using m3s02_auth.Utils;
using System.Xml;

namespace m3s02_auth.Services
{
    public class AutenticacaoServices : IAutenticacaoServices
    {
        private readonly IUsuarioService _usuarioService;

        public AutenticacaoServices(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public bool Autenticar(LoginDTO login)
        {
            var usuario = _usuarioService.ObterPorId(login.Usuario);
            if (usuario != null)
            {
                return usuario.Senha == Criptografia.CriptografarSenha(login.Senha);
              
            }
            return false;

        }
    
    }
}
