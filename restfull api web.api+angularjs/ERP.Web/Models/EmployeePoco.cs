using ERP.Core.Models;
using ERP.Web.Validators;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models
{
    [Validator(typeof(EmployeePocoValidator))]
    public class EmployeePoco
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public PositionPoco Position { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime? FiredDate { get; set; }
        public ICollection<PropertyPoco> Properties { get; set; }
        public ICollection<EmployeeToSkillPoco> Skills { get; set; }
        public ICollection<EmployeeToCertificatePoco> Certificates { get; set; }
    }
}