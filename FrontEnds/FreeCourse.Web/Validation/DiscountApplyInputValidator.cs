using FluentValidation;
using FreeCourse.Web.Models.Discount;

namespace FreeCourse.Web.Validation
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("İndirim kupon alanı boş olamaz.");
        }
    }
}
