using FluentValidation;
using Project.ViewModels.Models.RequestModels.Categories;

namespace Project.Validation.Validators.Categories
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequestModel>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id 0'dan büyük olmalıdır");

            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori adı boş olamaz")
                .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır")
                .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olabilir")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$").WithMessage("Kategori adı sadece harf içermelidir");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz")
                .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalıdır")
                .MaximumLength(200).WithMessage("Açıklama en fazla 200 karakter olabilir");
        }
    }
}