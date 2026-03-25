using AspMarketplace.Web.ViewModels.Admin;
using FluentValidation;

namespace AspMarketplace.Web.Validators.Admin;

public class ProductCategoryFormValidator : AbstractValidator<ProductCategoryFormViewModel>
{
    public ProductCategoryFormValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nama kategori wajib diisi.")
            .MaximumLength(100).WithMessage("Nama maksimal 100 karakter.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Deskripsi maksimal 500 karakter.");
    }
}
