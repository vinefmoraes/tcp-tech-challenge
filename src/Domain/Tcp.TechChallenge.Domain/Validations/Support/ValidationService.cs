namespace Tcp.TechChallenge.Domain.Validations.Support
{
    using FluentValidation.Results;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class ValidationService : IValidationService
    {
        private readonly IServiceProvider _serviceProvider;
        public ValidationService(IServiceProvider serviceProvider) 
            => _serviceProvider = serviceProvider;

        public ValidationResult Validate<TRequest>(TRequest request) where TRequest : class
        {
            return GetValidator<TRequest>().Validate(request);
        }

        private IValidator<TRequest> GetValidator<TRequest>()
            where TRequest : class
        {
            return _serviceProvider.GetService<IValidator<TRequest>>();
        }
    }
}
