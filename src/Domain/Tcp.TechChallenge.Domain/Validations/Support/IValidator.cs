using FluentValidation.Results;

namespace Tcp.TechChallenge.Domain.Validations.Support
{
    public interface IValidator<TRequest>
        where TRequest : class
    {
        public ValidationResult Validate(TRequest request);
    }
}
