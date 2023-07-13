using m3s02_auth.Model;

namespace m3s02_auth.DTO
{
    public class UsuarioDTO : UsuarioGetDTO
    {
        public string Senha { get; set; }

        public UsuarioDTO()
        {
            
        }
        public UsuarioDTO(Usuario usuario) : base (usuario)
        {
            Senha = usuario.Senha;
        }
    }
}
