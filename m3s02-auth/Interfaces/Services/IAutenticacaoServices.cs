using m3s02_auth.DTO;

namespace m3s02_auth.Interfaces.Services
{
    public interface IAutenticacaoServices
    {
        bool Autenticar(LoginDTO login);
        string GerarToken(LoginDTO loginDTO);
    }
}
