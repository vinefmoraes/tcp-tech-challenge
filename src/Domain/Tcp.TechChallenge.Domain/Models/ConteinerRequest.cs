using Tcp.TechChallenge.Domain.Enums;

namespace Tcp.TechChallenge.Domain.Models
{
    public class ConteinerRequest
    {
        public string Numero { get; set; }

        public TipoOperacao TipoOperacao { get; set; }

        public Capacidade Capacidade { get; set; }
    }
}
