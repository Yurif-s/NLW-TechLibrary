using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Application.UseCases.Users.Create;
public class CreateUserUseCase
{
    public ResponseCreatedUserJson Execute(RequestUserJson request)
    {
        Validate(request);

        return new ResponseCreatedUserJson
        {

        };
    }
    private void Validate(RequestUserJson request)
    {
        var validator = new CreateUserValidator();

        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorsMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
