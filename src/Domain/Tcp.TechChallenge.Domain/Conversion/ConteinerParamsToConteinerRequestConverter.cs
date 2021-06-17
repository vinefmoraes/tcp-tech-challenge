namespace Tcp.TechChallenge.Domain.Conversion
{
    using System;
    using Tcp.TechChallenge.Domain.Conversion.Support;
    using Tcp.TechChallenge.Domain.Enums;
    using Tcp.TechChallenge.Domain.Models;
    using Tcp.TechChallenge.Infra.Models;

    public class ConteinerParamsToConteinerRequestConverter
        : IConverter<Infra.Models.Conteiner, ConteinerRequest>
    {
        public ConteinerRequest Convert(Conteiner request)
        {
            return new ConteinerRequest
            {
                Capacity = (Capacidade)Enum.ToObject(typeof(Capacidade), request.Capacidade),
                Operation = (TipoOperacao)Enum.ToObject(typeof(TipoOperacao), request.TipoOperacao),
                Number = request.Numero
            };
        }
    }
}
