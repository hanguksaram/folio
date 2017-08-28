using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Moq;

namespace ERP.UnitTests
{
    public abstract class BaseTestClass
    {
        protected void SetControllerMocks(Controller controller)
        {
            var headerDictionary = new NameValueCollection();
            var cache = new Mock<HttpCachePolicyBase>();
            var response = new Mock<HttpResponseBase>();
            response.SetupGet(r => r.Cache).Returns(cache.Object);
            response.SetupGet(r => r.Headers).Returns(headerDictionary);
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(a => a.Response).Returns(response.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext.Object
            };
        }

        protected object GetFieldFromAnonymous(object obj, string fieldName)
        {
            Type t = obj.GetType();
            PropertyInfo p = t.GetProperty(fieldName);

            return p.GetValue(obj, null);
        }
    }
}
