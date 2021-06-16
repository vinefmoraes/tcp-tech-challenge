using System.Collections.Generic;
using System.Threading.Tasks;
using Tcp.TechChallenge.Domain.Models;

namespace Tcp.TechChallenge.Domain.Services.Impl
{
    public class ConteinerHandleService : IConteinerHandleService
    {
        public Task<bool> Delete(string conteinerIdentifier)
        {
            throw new System.NotImplementedException();
        }

        public Task<Conteiner> Edit(string conteinerIdentifier, Conteiner conteiner)
        {
            throw new System.NotImplementedException();
        }

        public Conteiner FindByIdentifier(string conteinerIdentifier)
        {
            return new Conteiner
            {
                Numero = "12983129387123",
                Capacidade = Enums.Capacidade.VINTE,
                TipoOperacao = Enums.TipoOperacao.Importacao
            };
        }

        public Task Insert(Conteiner conteiner)
        {
            throw new System.NotImplementedException();
        }

        public IList<Conteiner> List()
        {
            return new[]
            {
                new Conteiner
                {
                    Numero = "12983129387123",
                    Capacidade = Enums.Capacidade.VINTE,
                    TipoOperacao = Enums.TipoOperacao.Importacao
                },
                new Conteiner
                {
                    Numero = "23098328904",
                    Capacidade = Enums.Capacidade.QUARENTA,
                    TipoOperacao = Enums.TipoOperacao.Exportacao
                },
                new Conteiner
                {
                    Numero = "938824823984",
                    Capacidade = Enums.Capacidade.VINTE,
                    TipoOperacao = Enums.TipoOperacao.Transbordo
                },
                new Conteiner
                {
                    Numero = "16526534423",
                    Capacidade = Enums.Capacidade.QUARENTA,
                    TipoOperacao = Enums.TipoOperacao.Cabotagem
                }
            };
        }
    }
}
