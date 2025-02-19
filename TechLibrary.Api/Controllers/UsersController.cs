using Microsoft.AspNetCore.Mvc;
using TechLibrary.Application.UseCases.Users.Create;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreatedUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessages), StatusCodes.Status400BadRequest)]
    public IActionResult Create(RequestUserJson request)
    {
        try
        {
            var useCase = new CreateUserUseCase();

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }
        catch (ErrorOnValidationException ex)
        {
            return BadRequest(new ResponseErrorMessages
            {
                Errors = ex.GetErrorMessages()
            });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorMessages
            {
                Errors = ["Unknow error."]
            });
        }
    }
}
