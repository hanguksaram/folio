using ERP.Web.Models;
using FluentValidation;

namespace ERP.Web.Validators
{
    public class SkillPocoValidator : AbstractValidator<SkillPoco>
    {
        public SkillPocoValidator()
        {
            RuleFor(s => s.SkillName).NotNull().WithMessage("The SkillName field is required.");
            RuleFor(s => s.SkillName).NotEmpty().WithMessage("The SkillName field should not be empty.");
        }
    }
}