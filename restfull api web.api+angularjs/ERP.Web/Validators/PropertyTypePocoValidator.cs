using ERP.Web.Models;
using FluentValidation;

namespace ERP.Web.Validators
{
    public class PropertyTypePocoValidator : AbstractValidator<PropertyTypePoco>
    {
        public PropertyTypePocoValidator()
        {
            RuleFor(p => p.PropertyTypeName).NotNull().WithMessage("The PropertyTypeName field is required.");
            RuleFor(p => p.PropertyTypeName).NotEmpty().WithMessage("The PropertyTypeName field should not be empty.");
        }
    }
}