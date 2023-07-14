using m3s02_auth.DTO;
using m3s02_auth.Exceptions;
using m3s02_auth.Interfaces.Services;
using m3s02_auth.Utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using System;
using System.Xml;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace m3s02_auth.Services
{
    public class AutenticacaoServices : IAutenticacaoServices
    {
        private readonly IUsuarioService _usuarioService;

        private readonly string _chaveJwt ;

        public AutenticacaoServices(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;

            _chaveJwt = configuration.GetSection("jwtTokenChave").Get<string>();
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

        public string GerarToken(LoginDTO loginDTO)
        {
            var usuario = _usuarioService.ObterPorId(loginDTO.Usuario);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_chaveJwt);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                    new Claim(ClaimTypes.Name, usuario.Login),
                    new Claim(ClaimTypes.Role, usuario.Permissao),
              }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }
}
