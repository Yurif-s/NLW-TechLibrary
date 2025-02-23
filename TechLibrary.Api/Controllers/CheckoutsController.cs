using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Application.UseCases.Checkouts;
using TechLibrary.Communication.Responses;
using TechLibrary.Infrastructure.Services.LoggedUser;

namespace TechLibrary.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CheckoutsController : ControllerBase
{
    [HttpPost, Route("{bookId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status409Conflict)]
    public IActionResult BookCheckout(Guid bookId)
    {
        var httpContextAccessor = new HttpContextAccessor { HttpContext = HttpContext };

        var loggedUser = new LoggedUserService(httpContextAccessor);

        var useCase = new RegisterBookCheckoutUseCase(loggedUser);

        useCase.Execute(bookId);

        return NoContent();
    }
}
