using m3s02_auth.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace m3s02_auth.DTO
{
    public class UsuarioGetDTO
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Permissao { get; set; }
        public bool Interno { get; set; }


        public UsuarioGetDTO()
        { }

        public UsuarioGetDTO(Usuario usuario)
        {
            Nome = usuario.Nome;
            Login = usuario.Login;
            Permissao = usuario.Permissao;
            Interno = usuario.Interno;
        }
    }
}
