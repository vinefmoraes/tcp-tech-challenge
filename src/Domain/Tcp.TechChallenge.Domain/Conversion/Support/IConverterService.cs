namespace Tcp.TechChallenge.Domain.Conversion.Support
{
    public interface IConverterService
    {
        public void Convert<T, TResponse>(T request, out TResponse response)
            where T : class
            where TResponse : class;
    }
}
