namespace Tcp.TechChallenge.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Tcp.TechChallenge.Domain.Models;

    public interface IConteinerHandleService
    {
        ObjectResponse<ConteinerRequest> FindByIdentifier(string conteinerIdentifier);
        IList<ConteinerRequest> FindAll();

        Task<ObjectResponse<int>> Insert(ConteinerRequest conteiner);
        Task<ObjectResponse<bool>> Edit(string conteinerIdentifier, ConteinerRequest conteiner);
        Task<ObjectResponse<bool>> Delete(string conteinerIdentifier);
    }
}