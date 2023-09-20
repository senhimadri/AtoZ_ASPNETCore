using FluentValidation;

namespace FluentValidationPros;

public class StudentDTO
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; }= string.Empty;
    public string Email { get; set; }=string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int Age { get; set; }
}

public class StudentValidator: AbstractValidator<StudentDTO>
{
    public StudentValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Not valied.");
        RuleFor(x => x.Phone).Length(11);
        RuleFor(x=>x.Age).GreaterThanOrEqualTo(18);
    }
}
