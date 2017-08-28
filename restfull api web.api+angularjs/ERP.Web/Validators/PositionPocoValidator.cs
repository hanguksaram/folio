using ERP.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Validators
{
    public class PositionPocoValidator : AbstractValidator<PositionPoco>
    {
        public PositionPocoValidator()
        {
            RuleFor(p => p.PositionName).NotNull().WithMessage("The PositionName field is required.");
            RuleFor(p => p.PositionName).NotEmpty().WithMessage("The PositionName field should not be empty.");
        }
    }
}