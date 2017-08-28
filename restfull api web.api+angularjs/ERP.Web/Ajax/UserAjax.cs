using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP.Core.Models;
using Omu.ValueInjecter;

namespace ERP.Web.Ajax
{
    public class UserAjax:User
    {
        public UserAjax(User src)
        {
            this.InjectFrom(src);
            this.Companies = new List<Company>();
        }

        public bool Ticket { get; set; }
    }
}