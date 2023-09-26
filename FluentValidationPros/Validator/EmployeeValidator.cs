using FluentValidation;
using FluentValidationPros.Models;

namespace FluentValidationPros.Validator;

public class EmployeeValidator:AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(emp => emp.Surname).NotNull().WithMessage("Please ensure you have entered your {PropertyName}. {PropertyValue}.");
    }
}
