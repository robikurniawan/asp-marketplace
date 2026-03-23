using AspMarketplace.Web.ViewModels.Auth;
using FluentValidation;

namespace AspMarketplace.Web.Validators.Auth;

public class LoginValidator : AbstractValidator<LoginViewModel>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email wajib diisi.")
            .EmailAddress().WithMessage("Format email tidak valid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password wajib diisi.")
            .MinimumLength(6).WithMessage("Password minimal 6 karakter.");
    }
}
