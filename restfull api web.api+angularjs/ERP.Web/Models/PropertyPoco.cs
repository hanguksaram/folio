using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ERP.Web.Validators;
using FluentValidation.Attributes;

namespace ERP.Web.Models
{
    [Validator(typeof(PropertyPocoValidator))]
    public class PropertyPoco
    {
        public int ID { get; set; }
        public string PropertyValue { get; set; }
        public string  PropertyType { get; set; }
        public int PropertyTypeId { get; set; }
    }
}