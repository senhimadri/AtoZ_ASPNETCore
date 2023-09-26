using FluentValidation;
using FluentValidationPros.Models;

namespace FluentValidationPros.Validator;

public class CustomerValidator: AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(x=>x.Name).NotNull().NotEmpty();
        RuleFor(x=>x.Address).SetValidator(new AddressValidator());
    }
}

public class CustomerValidator2: AbstractValidator<Customer>
{
    public CustomerValidator2()
    {
        RuleFor(x => x.Address.Postcode).NotNull().When(x => x.Address != null);
    }
}
