using System.Collections.Generic;

namespace m3s02_auth.Interfaces.Repositories
{
    public interface IBaseRepository<TModel, TChave>
    {
        public TModel Inserir(TModel model);
        public TModel ObterPorId(TChave id);
        public TModel Atualizar(TModel model);
        public List<TModel> ObterTodos();
        public void Excluir(TModel model);
    }
}
