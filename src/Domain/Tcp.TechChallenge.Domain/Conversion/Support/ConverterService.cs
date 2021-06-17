using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tcp.TechChallenge.Domain.Conversion.Support
{
    public class ConverterService: IConverterService
    {
        private readonly IServiceProvider _serviceProvider;

        public ConverterService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Convert<T, TResponse>(T request, out TResponse response)
            where T : class
            where TResponse : class
        {
            response = GetConverter<T, TResponse>().Convert(request);
        }

        private IConverter<T, TResponse> GetConverter<T, TResponse>()
            where T: class
            where TResponse : class
        {
            return _serviceProvider.GetService<IConverter<T, TResponse>>();
        }
    }
}
