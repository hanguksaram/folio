using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ERP.Web.Ajax;

namespace ERP.Web.Controllers
{
    using ERP.Core.Models;
    using ERP.Core.Services;
    
    //todo: check roles after merging Skills to ERP
    //[Authorize(Roles = "sa")]
    public class UsersController : BaseController
    {
        private IUserService userService;
        public UsersController(IUserService uSrv)
        {
            this.userService = uSrv;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            var data = userService.GetUsers().ToList().Select(x => new UserAjax(x));
            return this.Success(data);
        }
       
        private string RandomString(int size)
        {   Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(48 + 42 * random.NextDouble() )));
                builder.Append(ch);
            }

            return builder.ToString();
        }
        public JsonResult Save(User data)
        {

            data.Salt = RandomString(10);

            data.Password = userService.GetPasswordCode(data.Password, data.Salt);

            userService.Update(data);
            return this.Success();
        }

        public JsonResult Remove(int id)
        {
            userService.Delete(id);
            return this.Success();
        }
	}
}