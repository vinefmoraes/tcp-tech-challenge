namespace Tcp.TechChallenge.Domain.Validations.Support
{
    using FluentValidation;
    using FluentValidation.Results;
    using System;

    public abstract class AbstractValidation<TRequest> : AbstractValidator<TRequest>, IValidator<TRequest>
        where TRequest: class
    {
        public abstract ValidationResult ValidateTarget(TRequest request);
        
        public new ValidationResult ValidateRequest(TRequest request)
        {
            return ValidateTarget(request);
        }
    }
}
