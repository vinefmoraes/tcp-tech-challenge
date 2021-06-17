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
                Capacidade = (Capacidade)Enum.ToObject(typeof(Capacidade), request.Capacidade),
                TipoOperacao = (TipoOperacao)Enum.ToObject(typeof(TipoOperacao), request.TipoOperacao),
                Numero = request.Numero
            };
        }
    }
}
