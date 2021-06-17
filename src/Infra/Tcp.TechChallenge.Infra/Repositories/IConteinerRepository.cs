namespace Tcp.TechChallenge.Infra.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Tcp.TechChallenge.Infra.Models;

    public interface IConteinerRepository
    {
        public Task Save(Conteiner conteiner);
        public Task<bool> Delete(string identifier);
        public Task Edit(Conteiner conteiner);
        public IList<Conteiner> FindAll();
        public Conteiner FindById(string identifier);
    }
}