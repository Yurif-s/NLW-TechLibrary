using Microsoft.AspNetCore.Mvc;
using TechLibrary.Application.UseCases.Books.Filter;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    [HttpGet("Filter")]
    [ProducesResponseType(typeof(ResponseBooksJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Filter(int pageNumber, string? title)
    { 
        var useCase = new FilterBookUseCase();

        var request = new RequestFilterBooksJson
        {
            PageNumber = pageNumber,
            Title = title,
        };

        var result = useCase.Execute(request);

        return Ok(result);
    }
}
