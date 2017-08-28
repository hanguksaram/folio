using ERP.Core.Models;
using ERP.Web.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Mappers
{
    public static class MapperPropertyTypePoco
    {
        public static PropertyTypePoco MapToPropertyTypePoco(this PropertyType propertyType)
        {
            return new PropertyTypePoco().InjectFrom(propertyType) as PropertyTypePoco;
        }

        public static PropertyType MapToPropertyType(this PropertyTypePoco propertyTypePoco)
        {
            return new PropertyType().InjectFrom(propertyTypePoco) as PropertyType;
        }
    }
}