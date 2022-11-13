using FluentValidation;

namespace Application.MediatR.Users.Accounts.Commands.RefreshPassword
{
    public class RefreshPasswordCommandValidator : AbstractValidator<RefreshPasswordCommand>
    {
        public RefreshPasswordCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Поле является обязательным к заполнению")
                .EmailAddress().WithMessage("Недопустимый email");

            RuleFor(v => v.PhoneNumber)
                .NotEmpty().WithMessage("Поле является обязательным к заполнению");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Поле является обязательным к заполнению")
                .Equal(c => c.Password).WithMessage("Пароли не совпадают")
                .MinimumLength(8).WithMessage("Пароль должен быть не менее 8 символов");
        }
    }
}