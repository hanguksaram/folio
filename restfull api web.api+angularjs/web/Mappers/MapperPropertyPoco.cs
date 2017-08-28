using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP.Core.Models;
using ERP.Web.Models;
using Omu.ValueInjecter;

namespace ERP.Web.Mappers
{
    public static class MapperPropertyPoco
    {
        public static PropertyPoco MapToPropertyPoco(this Property property)
        {
            var propertyPoco = new PropertyPoco();

            propertyPoco.InjectFrom(property);
            if (property.PropertyType != null)
            {
                //propertyPoco.PropertyType = property.PropertyType.MapToPropertyTypePoco();
                //TODO:
            }
            
            return propertyPoco;
        }

        public static Property MapToProperty(this PropertyPoco propertyPoco)
        {
            var property = new Property();

            property.InjectFrom(propertyPoco);
            if (propertyPoco.PropertyType != null)
            {
                //property.PropertyType = propertyPoco.PropertyType.MapToPropertyType();
                //TODO:
            }

            return property;
        }
    }
}