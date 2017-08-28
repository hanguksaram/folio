using ERP.Web.Models;
using FluentValidation;

namespace ERP.Web.Validators
{
    public class GroupOfSkillsPocoValidator : AbstractValidator<GroupOfSkillsPoco>
    {
        public GroupOfSkillsPocoValidator()
        {
            RuleFor(g => g.GroupName).NotNull().WithMessage("The GroupName field is required.");
            RuleFor(g => g.GroupName).NotEmpty().WithMessage("The GroupName field should not be empty.");
        }
    }
}