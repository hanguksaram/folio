using ERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP.Web.Validators;
using FluentValidation.Attributes;

namespace ERP.Web.Models
{
    [Validator(typeof(EmployeeToSkillPocoValidator))]
    public class EmployeeToSkillPoco
    {
        public int ID { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int Level { get; set; }
        public int Preference { get; set; }
    }
}