using ERP.Web.Validators;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models
{
    [Validator(typeof(CertificatePocoValidator))]
    public class CertificatePoco
    {
        public int ID { get; set; }
        public string CertificateName { get; set; }
    }
}