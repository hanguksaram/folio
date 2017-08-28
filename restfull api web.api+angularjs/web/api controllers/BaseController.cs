using System;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        [NonAction]
        public JsonResult Success(object data = null)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Headers.Set("Cache-Control", "private, max-age=0");
            var jsonResult = this.Json(new { success = true, response = data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [NonAction]
        public JsonResult Error(object result)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Headers.Set("Cache-Control", "private, max-age=0");
            return this.Json(new { success = false, response = result }, JsonRequestBehavior.AllowGet);
        }
	}
}