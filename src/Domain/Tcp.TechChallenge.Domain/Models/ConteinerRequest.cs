using Tcp.TechChallenge.Domain.Enums;

namespace Tcp.TechChallenge.Domain.Models
{
    public class ConteinerRequest
    {
        public string? Number { get; set; }
        public TipoOperacao? Operation { get; set; }
        public Capacidade? Capacity { get; set; }
    }
}
