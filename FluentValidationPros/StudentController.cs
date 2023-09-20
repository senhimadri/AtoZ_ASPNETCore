using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
namespace FluentValidationPros;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    public readonly IValidator<StudentDTO> _validator;

    public static List<StudentDTO> _studentDTOs = new List<StudentDTO>();

    public StudentController(IValidator<StudentDTO> validator)
    {
        _validator = validator;
    }

    [HttpGet]
    [Route("AddStudent")]
    public dynamic AddStudent( )
    {
        return "Get";


    }

    [HttpPost]
    [Route("AddStudent")]
    public dynamic AddStudent (StudentDTO studentDTO)
    {
        var validator = _validator.Validate(studentDTO);

        if (validator.IsValid)
        {
            _studentDTOs.Add(studentDTO);
            return "Successfully Added";
        }

        else
        {
           
            return validator.Errors;
        }


    }
}
