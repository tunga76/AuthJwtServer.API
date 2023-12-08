using AuthJwtServer.API.Dtos;
using FluentValidation;

namespace AuthJwtServer.API.Validations
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("EMail reguired").EmailAddress().WithMessage("Email is not invalid");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName reguired");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id Reguired");
        }
    }
}
