using FluentValidation;

namespace Ekmob.TechSession.Application.Commands.CustomerCreate
{
    public class CustomerCreateValidator : AbstractValidator<CustomerCreateCommand>
    {
        public CustomerCreateValidator()
        {
            RuleFor(v => v.IdentityNumber)
                .EmailAddress()
                .NotEmpty();

            RuleFor(v => v.DepartmentName)
                .NotEmpty();
        }
    }
}
