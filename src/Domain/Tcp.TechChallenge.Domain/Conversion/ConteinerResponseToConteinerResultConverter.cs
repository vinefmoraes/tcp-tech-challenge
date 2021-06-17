using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tcp.TechChallenge.Domain.Conversion.Support;
using Tcp.TechChallenge.Domain.Enums;
using Tcp.TechChallenge.Domain.Models;
using Tcp.TechChallenge.Infra.Models;

namespace Tcp.TechChallenge.Domain.Conversion
{
    public class ConteinerResponseToConteinerResultConverter
         : IConverter<IList<Infra.Models.Conteiner>, IList<ConteinerRequest>>
    {
        public IList<ConteinerRequest> Convert(IList<Conteiner> request)
        {
            return request.Select(c => new ConteinerRequest
            {
                Capacity = (Capacidade)Enum.ToObject(typeof(Capacidade), c.Capacidade),
                Operation = (TipoOperacao)Enum.ToObject(typeof(TipoOperacao), c.TipoOperacao),
                Number = c.Numero
            }).ToList();
        }
    }
}
