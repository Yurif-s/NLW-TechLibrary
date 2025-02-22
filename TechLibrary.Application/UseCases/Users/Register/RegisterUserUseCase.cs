using FluentValidation.Results;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Entities;
using TechLibrary.Exception;
using TechLibrary.Infrastructure.DataAccess;
using TechLibrary.Infrastructure.Security.Cryptography;
using TechLibrary.Infrastructure.Tokens.Access;

namespace TechLibrary.Application.UseCases.Users.Create;
public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestUserJson request)
    {
        var dbContext = new TechLibraryDbContext();

        Validate(request, dbContext);

        var cryptography = new BCryptAlgorithm();

        var entity = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = cryptography.HashPassword(request.Password)
        };

        dbContext.Users.Add(entity);
        dbContext.SaveChanges();

        var tokenGenerator = new JwtTokenGenerator();

        return new ResponseRegisteredUserJson
        {
            Name = entity.Name,
            AccessToken = tokenGenerator.Generate(entity)
        };
    }
    private void Validate(RequestUserJson request, TechLibraryDbContext dbContext)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var existUserWithEmail = dbContext.Users.Any(user => user.Email.Equals(request.Email));
        if (existUserWithEmail)
            result.Errors.Add(new ValidationFailure("Email", "E-mail already registered on the platform."));

        if(result.IsValid == false)
        {
            var errorsMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
