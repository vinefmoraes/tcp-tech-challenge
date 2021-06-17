namespace Tcp.TechChallenge.Domain.Validations
{
    using FluentValidation;
    using FluentValidation.Results;
    using Tcp.TechChallenge.Domain.Models;
    using Tcp.TechChallenge.Domain.Validations.Support;

    public class ConteinerValidation: AbstractValidation<ConteinerRequest> 
    {
        public ConteinerValidation()
        {
            RuleFor(x => x.Number)
                .NotEmpty()
                .Length(11).WithMessage("Número deve ter exatamente 11 digitos")
                .Matches("^\\d{11}$").WithMessage("Identificador deve ter apenas números");

            RuleFor(x => x.Capacity)
                .NotNull().WithMessage("Capacidade não deve ser nula")
                .IsInEnum().WithMessage("Capacidade deve ser um valor válido");

            RuleFor(x => x.Operation)
                .NotNull().WithMessage("Operação não deve ser nula")
                .IsInEnum().WithMessage("Operação deve ser um valor válido");
        }

        public override ValidationResult ValidateTarget(ConteinerRequest request)
        {
            return base.Validate(request);
        }
    }
}
