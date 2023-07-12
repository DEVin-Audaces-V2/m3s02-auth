using Microsoft.AspNetCore.SignalR.Protocol;

namespace m3s02_auth.Model
{
    public class TokenCliente
    {
        public string Token { get; set; }
        public string NomeCliente { get; set; }
        public int IdCliente { get; set; }

    }
}
