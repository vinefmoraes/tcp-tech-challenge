namespace Tcp.TechChallenge.Domain.Conversion
{
    using Tcp.TechChallenge.Domain.Conversion.Support;
    using Tcp.TechChallenge.Domain.Models;

    public class ConteinerRequestToConteinerParamsConverter
        : IConverter<ConteinerRequest, Infra.Models.Conteiner>
    {
        public Infra.Models.Conteiner Convert(ConteinerRequest request)
        {
            return new Infra.Models.Conteiner{
                Numero = request.Numero,
                Capacidade = (short)request.Capacidade,
                TipoOperacao = (short)request.TipoOperacao
            };
        }
    }
}
