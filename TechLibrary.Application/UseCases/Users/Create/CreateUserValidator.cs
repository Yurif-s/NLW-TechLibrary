using FluentValidation;
using TechLibrary.Communication.Requests;

namespace TechLibrary.Application.UseCases.Users.Create;
public class CreateUserValidator : AbstractValidator<RequestUserJson>
{
    public CreateUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("The name is required.");
        RuleFor(user => user.Email).EmailAddress().WithMessage("The email is not valid.");
        RuleFor(user => user.Password).MinimumLength(8).WithMessage("The password is not valid.");
    }
}
