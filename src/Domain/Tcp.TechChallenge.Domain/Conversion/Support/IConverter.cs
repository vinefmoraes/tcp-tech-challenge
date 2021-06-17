namespace Tcp.TechChallenge.Domain.Conversion.Support
{
    public interface IConverter<TRequest, TResponse> 
        where TRequest : class
        where TResponse : class
    {
        public TResponse Convert(TRequest request);
    }
}
