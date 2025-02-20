using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Entities;
using TechLibrary.Exception;
using TechLibrary.Infrastructure;

namespace TechLibrary.Application.UseCases.Users.Create;
public class CreateUserUseCase
{
    public ResponseCreatedUserJson Execute(RequestUserJson request)
    {
        Validate(request);

        var entity = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };

        var dbContext = new TechLibraryDbContext();

        dbContext.Users.Add(entity);
        dbContext.SaveChanges();

        return new ResponseCreatedUserJson
        {
            Name = entity.Name
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
