using FluentValidation;

namespace Application.MediatR.Users.Accounts.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.Email)
               .NotEmpty().WithMessage("Поле является обязательным к заполнению");
            RuleFor(c => c.Password)
               .NotEmpty().WithMessage("Поле является обязательным к заполнению")
               .MinimumLength(8).WithMessage("Пароль должен быть не менее 8 символов");
        }
    }
}