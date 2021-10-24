using FluentValidation;

namespace Ekmob.TechSession.Application.Commands.CustomerCreate
{
    public class CustomerCreateValidator : AbstractValidator<CustomerCreateCommand>
    {
        public CustomerCreateValidator()
        {
            RuleFor(v => v.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(v => v.Name)
                .NotEmpty();
        }
    }
}
