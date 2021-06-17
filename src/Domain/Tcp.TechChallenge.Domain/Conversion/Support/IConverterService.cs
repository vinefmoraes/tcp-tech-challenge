namespace Tcp.TechChallenge.Domain.Conversion.Support
{
    public interface IConverterService
    {
        public void TryConvert<T, TResponse>(T request, out TResponse response)
            where T : class
            where TResponse : class;

        public TResponse Convert<T, TResponse>(T request)
            where T : class
            where TResponse : class;
    }
}
