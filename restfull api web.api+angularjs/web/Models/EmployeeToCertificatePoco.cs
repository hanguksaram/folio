using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP.Web.Validators;
using FluentValidation.Attributes;

namespace ERP.Web.Models
{
    [Validator(typeof(EmployeeToCertificateValidator))]
    public class EmployeeToCertificatePoco
    {
        public int ID { get; set; }
        public int CertificateId { get; set; }
        public string CertificateName { get; set; }
        public string Comment { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool? Pending { get; set; }
    }
}