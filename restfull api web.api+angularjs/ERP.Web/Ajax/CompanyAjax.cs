using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using ERP.Core.Models;
using Omu.ValueInjecter;

namespace ERP.Web.Ajax
{
    public class CompanyAjax: Company
    {
        public CompanyAjax(Company source)
        {
            this.InjectFrom(source);
            this.Managers = new List<User>();
        }
    }
}