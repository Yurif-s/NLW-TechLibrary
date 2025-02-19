using Microsoft.AspNetCore.Mvc;
using TechLibrary.Application.UseCases.Users.Create;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreatedUserJson), StatusCodes.Status201Created)]
    public IActionResult Create(RequestUserJson request)
    {
        var useCase = new CreateUserUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
