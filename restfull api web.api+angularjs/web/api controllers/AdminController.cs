using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Controllers
{

    //todo: check roles after merging Skills to ERP
    //[Authorize(Roles = "sa")]
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
	}
}