using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Application.UseCases.Users.Create;
public class CreateUserUseCase
{
    public ResponseCreatedUserJson Execute(RequestUserJson request)
    {
        return new ResponseCreatedUserJson
        {

        };
    }
}
