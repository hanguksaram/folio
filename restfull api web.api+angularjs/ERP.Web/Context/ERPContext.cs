using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Context
{
    //IsAdmin == user type enum?
    public class ERPContext
    {
        public int UserId { get; set; }
        public bool IsAuth { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }        
    }
}