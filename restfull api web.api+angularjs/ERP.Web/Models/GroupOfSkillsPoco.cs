using ERP.Web.Validators;
using FluentValidation.Attributes;
using ERP.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Web.Models
{
    [Validator(typeof(GroupOfSkillsPocoValidator))]
    public class GroupOfSkillsPoco
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<SkillPoco> Skills { get; set; }
    }
}
