using FluentValidation;
using TechLibrary.Communication.Requests;

namespace TechLibrary.Application.UseCases.Users.Create;
public class CreateUserValidator : AbstractValidator<RequestUserJson>
{
    public CreateUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("The name is required.");
        RuleFor(user => user.Email).EmailAddress().WithMessage("The email is not valid.");
        RuleFor(user => user.Password).NotEmpty().WithMessage("The password is required.");
        When(user => string.IsNullOrWhiteSpace(user.Password) == false, () =>
        {
            RuleFor(user => user.Password.Length).GreaterThan(6).WithMessage("Password must be longer than 6 characters")
        });

    }
}
