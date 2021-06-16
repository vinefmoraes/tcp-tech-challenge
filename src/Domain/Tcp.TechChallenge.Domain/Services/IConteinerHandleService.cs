using System.Collections.Generic;
using System.Threading.Tasks;
using Tcp.TechChallenge.Domain.Models;

namespace Tcp.TechChallenge.Domain.Services
{
    public interface IConteinerHandleService
    {
        Conteiner FindByIdentifier(string conteinerIdentifier);

        Task Insert(Conteiner conteiner);
        Task<Conteiner> Edit(string conteinerIdentifier, Conteiner conteiner);
        Task<bool> Delete(string conteinerIdentifier);
        IList<Conteiner> List();
    }
}