using ERP.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Validators
{
    public class EmployeePocoValidator : AbstractValidator<EmployeePoco>
    {
        public EmployeePocoValidator()
        {
            RuleFor(e => e.Position).NotNull().WithMessage("The Position field is required.");
            RuleFor(e => e.BirthDate).NotNull().WithMessage("The BirthDate field is required.");
            RuleFor(e => e.HiringDate).NotNull().WithMessage("The HiringDate field is required.");
        }
    }
}