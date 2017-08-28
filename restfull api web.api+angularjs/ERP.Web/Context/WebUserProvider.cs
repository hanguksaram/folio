using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Context
{
    using ERP.Core;

    public class WebUserProvider : IAuthProvider
    {
        public string CurrentUser
        {
            get
            {

                if (AppContext.Current.IsAdmin)
                {
                    return "SA";
                }

                return AppContext.Current.UserId.ToString();
            }
            set
            {

            }
        }

        public bool IsAuth 
        { 
            get
            {
                return AppContext.Current.IsAuth;
            }
            set { }
        }

        public int UserId
        {
            get
            {
                return AppContext.Current.UserId;
            }
            set { }
        }
    }
}