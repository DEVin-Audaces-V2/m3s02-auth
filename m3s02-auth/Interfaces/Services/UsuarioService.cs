using m3s02_auth.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace m3s02_auth.Interfaces.Services
{
    public interface IUsuarioService
    {
        public Usuario Criar(Usuario usuario);
        public Usuario ObterPorId(string login);
        public Usuario Atualizar(Usuario usuario);
        public List<Usuario> Obter();
        public void Deletar(string login);
    }
}
