using FluentValidation;
using FluentValidationPros.Models;

namespace FluentValidationPros.Validator;
public class AddressValidator: AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Postcode).NotNull();
    }
}
