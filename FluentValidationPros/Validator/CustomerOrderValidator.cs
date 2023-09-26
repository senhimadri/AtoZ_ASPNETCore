using FluentValidation;
using FluentValidationPros.Models;

namespace FluentValidationPros.Validator;

public class CustomerOrderValidator: AbstractValidator<CustomerOrder>
{
    public CustomerOrderValidator()
    {
        RuleForEach(x => x.Orders).SetValidator(new OrderListValidator());

        //ChildRules
        RuleForEach(x => x.Orders).ChildRules(order =>
        {
            order.RuleFor(x=>x.Total).GreaterThan(0);
        });

        // Where and Foreach Rules
        RuleForEach(x => x.Orders).Where(x => x.Total != 0).SetValidator(new OrderListValidator());

        RuleFor(x => x.Orders).Must(x => x.Count() <= 10).WithMessage("No more that 10 order are allowed.")
            .ForEach(orderRule=>
            {
                orderRule.Must(x => x.Total > 0).WithMessage("Orders must have a total of more than 0.");
            });
    }
}
