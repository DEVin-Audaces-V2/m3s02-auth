namespace m3s02_auth.Model
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Permissao{ get; set; }
        public string Senha { get; set; }

        public void Update (Usuario usuario)
        {
            Nome = usuario.Nome;
            Permissao = usuario.Permissao;
            Senha = usuario.Senha;  
        }
    }
}
