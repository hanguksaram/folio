using ERP.Web.Models;
using FluentValidation;

namespace ERP.Web.Validators
{
    public class EmployeeToSkillPocoValidator : AbstractValidator<EmployeeToSkillPoco>
    {
        public EmployeeToSkillPocoValidator()
        {
            RuleFor(s => s.SkillId).NotEqual(0).WithMessage("SkillId should not be equal to 0.");

            RuleFor(s => s.Level).GreaterThanOrEqualTo(0).WithMessage("Level must be greater than or equal to 0.");
            RuleFor(s => s.Level).LessThanOrEqualTo(5).WithMessage("Level must be less than or equal to 5.");

            RuleFor(s => s.Preference).GreaterThanOrEqualTo(0).WithMessage("Preference must be greater than or equal to 0.");
            RuleFor(s => s.Preference).LessThanOrEqualTo(2).WithMessage("Preference must be less than or equal to 2.");
        }
    }
}