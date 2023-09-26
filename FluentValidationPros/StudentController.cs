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
    public async Task<IActionResult> AddStudent (StudentDTO studentDTO)
    {
        var validator = await _validator.ValidateAsync(studentDTO);


        //await _validator.ValidateAndThrowAsync(studentDTO);

        //var _validator_2 =await  _validator.ValidateAsync(studentDTO,options=> options.ThrowOnFailures());

        if (validator.IsValid)
        {
            _studentDTOs.Add(studentDTO);
            return Ok("Successfully Added.");
        }

        else
        {

            throw new Exception(validator.Errors.ToString());
        }
    }
}
