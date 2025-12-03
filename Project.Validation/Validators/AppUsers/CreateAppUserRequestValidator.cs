using FluentValidation;
using Project.ViewModels.Models.RequestModels.AppUsers;

namespace Project.Validation.Validators.AppUsers
{
    public class CreateAppUserRequestValidator : AbstractValidator<CreateAppUserRequestModel>
    {
        public CreateAppUserRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır")
                .MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olabilir")
                .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Kullanıcı adı sadece harf, rakam ve alt çizgi içerebilir");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır")
                .MaximumLength(50).WithMessage("Şifre en fazla 50 karakter olabilir")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermelidir");
        }
    }
}