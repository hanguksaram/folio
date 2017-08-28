using ERP.Web.Models;
using FluentValidation;

namespace ERP.Web.Validators
{
    public class PropertyPocoValidator : AbstractValidator<PropertyPoco>
    {
        public PropertyPocoValidator()
        {
            RuleFor(p => p.PropertyValue).NotNull().WithMessage("The PropertyValue field is required.");
            RuleFor(p => p.PropertyValue).NotEmpty().WithMessage("The PropertyValue field should not be empty.");

            RuleFor(p => p.PropertyTypeId).NotEqual(0).WithMessage("PropertyTypeId should not be equal to 0.");
        }
    }
}