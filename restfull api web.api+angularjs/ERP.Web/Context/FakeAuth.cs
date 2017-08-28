using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Context
{
    using ERP.Core;

    public class FakeAuth : IAuthProvider
    {
        public string CurrentUser
        {
            get
            {
                return "CONTEXT";
            }
            set
            {

            }
        }

        public bool IsAuth { get; set; }

        public int UserId { get; set; }

    }
}