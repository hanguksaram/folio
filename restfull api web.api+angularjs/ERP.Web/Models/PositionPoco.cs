using ERP.Web.Validators;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Web.Models
{
    [Validator(typeof(PositionPocoValidator))]
    public class PositionPoco
    {
        public int ID { get; set; }
        public string PositionName { get; set; }
    }
}