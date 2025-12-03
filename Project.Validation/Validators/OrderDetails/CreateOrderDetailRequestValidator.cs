using FluentValidation;
using Project.ViewModels.Models.RequestModels.OrderDetails;

namespace Project.Validation.Validators.OrderDetails
{
    public class CreateOrderDetailRequestValidator : AbstractValidator<CreateOrderDetailRequestModel>
    {
        public CreateOrderDetailRequestValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Sipariş Id 0'dan büyük olmalıdır");

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Ürün Id 0'dan büyük olmalıdır");
        }
    }
}