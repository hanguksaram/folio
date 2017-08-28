using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ERP.Web.Validators;
using FluentValidation.Attributes;

namespace ERP.Web.Models
{
    [Validator(typeof(PropertyTypePocoValidator))]
    public class PropertyTypePoco
    {
        public int ID { get; set; }
        public string PropertyTypeName { get; set; }
    }
}