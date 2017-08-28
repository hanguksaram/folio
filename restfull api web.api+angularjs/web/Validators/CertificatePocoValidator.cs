using ERP.Web.Models;
using FluentValidation;

namespace ERP.Web.Validators
{
    public class CertificatePocoValidator : AbstractValidator<EmployeeToCertificatePoco>
    {
        public CertificatePocoValidator()
        {
            RuleFor(c => c.CertificateName).NotNull().WithMessage("The CertificateName field is required.");
            RuleFor(c => c.CertificateName).NotEmpty().WithMessage("The CertificateName field should not be empty.");
        }
    }
}