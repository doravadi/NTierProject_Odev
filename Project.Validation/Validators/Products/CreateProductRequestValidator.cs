using FluentValidation;
using Project.ViewModels.Models.RequestModels.Products;

namespace Project.Validation.Validators.Products
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequestModel>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Ürün adı boş olamaz")
                .MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalıdır")
                .MaximumLength(100).WithMessage("Ürün adı en fazla 100 karakter olabilir");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Birim fiyat 0'dan büyük olmalıdır")
                .LessThanOrEqualTo(1000000).WithMessage("Birim fiyat 1.000.000'dan küçük veya eşit olmalıdır");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori Id 0'dan büyük olmalıdır")
                .When(x => x.CategoryId.HasValue);
        }
    }
}