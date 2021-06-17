namespace Tcp.TechChallenge.Domain.Validations.Support
{
    using FluentValidation.Results;

    public interface IValidationService
    {
        public ValidationResult Validate<TRequest>(TRequest request)
           where TRequest : class;
    }
}
