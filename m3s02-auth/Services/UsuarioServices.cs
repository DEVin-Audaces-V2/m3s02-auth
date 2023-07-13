using m3s02_auth.Interfaces.Repositories;
using m3s02_auth.Interfaces.Services;
using m3s02_auth.Model;
using m3s02_auth.Utils;
using System.Collections.Generic;

namespace m3s02_auth.Services
{
    public class UsuarioServices : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public Usuario Atualizar(Usuario usuario)
        {
            
            var usuarioDb = ObterPorId(usuario.Login);
            if (usuarioDb == null)
                throw new KeyNotFoundException("Usuario Não existe");

            usuarioDb.Update(usuario);
            if(!string.IsNullOrEmpty(usuario.Senha))
                usuarioDb.Senha = Criptografia.CriptografarSenha(usuario.Senha);
            _usuarioRepository.Atualizar(usuarioDb);        
            return usuario;
        }

        public Usuario Criar(Usuario usuario)
        {
            usuario.Senha = Criptografia.CriptografarSenha(usuario.Senha);
            return _usuarioRepository.Inserir(usuario);
        }

        public void Deletar(string login)
        {
            var usuarioDb =ObterPorId(login);
            if (usuarioDb == null)
                throw new KeyNotFoundException("Usuario Nõa existe");

            _usuarioRepository.Excluir(usuarioDb);
        }

        public List<Usuario> Obter()
        {
            return _usuarioRepository.ObterTodos();
        }

        public Usuario ObterPorId(string login)
        {
            return _usuarioRepository.ObterPorId(login);
        }
    }
}
