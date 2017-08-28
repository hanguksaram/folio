using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Zaybu.Compare;

namespace ERP.Core.HistoryStuff
{
    public class HsPropertyPoco
    {
        public string PropertyValue { get; set; }
        [KeyProperty]
        public string PropertyType { get; set; }  
    }
}