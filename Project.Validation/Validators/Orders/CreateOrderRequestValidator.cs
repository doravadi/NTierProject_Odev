using FluentValidation;
using Project.ViewModels.Models.RequestModels.Orders;

namespace Project.Validation.Validators.Orders
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequestModel>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.ShippingAddress)
                .NotEmpty().WithMessage("Teslimat adresi boş olamaz")
                .MinimumLength(10).WithMessage("Teslimat adresi en az 10 karakter olmalıdır")
                .MaximumLength(200).WithMessage("Teslimat adresi en fazla 200 karakter olabilir");

            RuleFor(x => x.AppUserId)
                .GreaterThan(0).WithMessage("Kullanıcı Id 0'dan büyük olmalıdır")
                .When(x => x.AppUserId.HasValue);
        }
    }
}