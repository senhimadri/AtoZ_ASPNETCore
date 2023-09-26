using FluentValidation;
using FluentValidationPros.Models;

namespace FluentValidationPros.Validator;

public class OrderListValidator: AbstractValidator<OrderList>
{
    public OrderListValidator()
    {
        RuleFor(x => x.Total).GreaterThan(0);
    }
}
