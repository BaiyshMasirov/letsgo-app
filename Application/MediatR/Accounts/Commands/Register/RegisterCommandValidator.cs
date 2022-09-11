using FluentValidation;

namespace Application.MediatR.Accounts.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(v => v.Name)
               .NotEmpty().WithMessage("Поле является обязательным к заполнению");
            RuleFor(v => v.Email)
               .NotEmpty().WithMessage("Поле является обязательным к заполнению")
               .EmailAddress().WithMessage("Недействительная почта");
            RuleFor(v => v.PhoneNumber)
               .NotEmpty().WithMessage("Поле является обязательным к заполнению")
               .MinimumLength(10).WithMessage("Минимальная длина телефона должна составлять 10 символов")
               .MaximumLength(15).WithMessage("Максимальная длина телефона должна составлять 15 символов");
            RuleFor(v => v.Password)
               .NotEmpty().WithMessage("Поле является обязательным к заполнению")
               .MinimumLength(8).WithMessage("Пароль должен быть не менее 8 символов");
            RuleFor(c => c.ConfirmPassword)
               .NotEmpty().WithMessage("Поле является обязательным к заполнению")
                .Equal(c => c.Password).WithMessage("Пароли не совпадают")
               .MinimumLength(8).WithMessage("Пароль должен быть не менее 8 символов");
        }
    }
}