using m3s02_auth.Interfaces.Repositories;
using m3s02_auth.Model;

namespace m3s02_auth.DataBase.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario, string>, IUsuarioRepository
    {
        public UsuarioRepository(DbContexto contexto) : base(contexto)
        {
        }
    }
}
