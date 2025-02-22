using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;
using TechLibrary.Infrastructure.DataAccess;
using TechLibrary.Infrastructure.Security.Cryptography;
using TechLibrary.Infrastructure.Tokens.Access;

namespace TechLibrary.Application.UseCases.Login.DoLogin;
public class DoLoginUseCase
{
    public ResponseRegisteredUserJson Execute(RequestLoginJson request)
    {
        var dbContext = new TechLibraryDbContext();

        var entity = dbContext.Users.FirstOrDefault(user => user.Email.Equals(request.Email));
         
        if(entity is null)
            throw new InvalidLoginException();

        var cryptography = new BCryptAlgorithm();
        var passwordIsValid = cryptography.Verify(request.Password, entity);

        if(passwordIsValid == false)
            throw new InvalidLoginException();

        var tokenGenerator = new JwtTokenGenerator();

        return new ResponseRegisteredUserJson
        {
             Name = entity.Name,
             AccessToken = tokenGenerator.Generate(entity)
        };
    }
}
