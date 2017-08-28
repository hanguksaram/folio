using ERP.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Validators
{
    public class EmployeeToCertificateValidator : AbstractValidator<EmployeeToCertificatePoco>
    {
        public EmployeeToCertificateValidator()
        {
            RuleFor(c => c.CertificateId).NotEqual(0).WithMessage("CertificateId should not be equal to 0.");
        }
    }
}