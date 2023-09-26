using FluentValidation;
using FluentValidationPros.Models;

namespace FluentValidationPros.Validator;
public class PersonValidator: AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleForEach(x=>x.AddressLine).NotNull().WithMessage("Address {CollectionIndex} is required.");
    }
}
