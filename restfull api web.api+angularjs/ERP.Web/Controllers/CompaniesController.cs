using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Core.Models;
using ERP.Core.Services;
using ERP.Web.Ajax;

namespace ERP.Web.Controllers
{
    public class CompaniesController : BaseController
    {
        private ICompanyService companyService;
        private IUserService userService;
        public CompaniesController(ICompanyService cSrv, IUserService uSrv)
        {
            this.companyService = cSrv;
            this.userService = uSrv;
        }

        public ActionResult Index()
        {
            return View();
        }

        //todo: fix when merge
        public JsonResult GetCompanyUsers(int id)
        {
            throw new Exception("This functionality is used in ERP only.");
        }

        //todo: fix when merge
        [HttpPost]
        public JsonResult AssignUser(/*Author data*/)
        {
            throw new Exception("This functionality is used in ERP only.");
        }

        public JsonResult Get()
        {
            var data = companyService.GetAll().ToList().Select(x => new CompanyAjax(x));
            return this.Success(data);
        }

        public ActionResult Edit(int id)
        {
            var company = companyService.Get(id) ?? new Company() {Name = "New Company"};
            ViewBag.Managers = company.Managers == null ? new List<UserAjax>(): company.Managers.Select(x => new UserAjax(x));            
            ViewBag.AvalibleUsers = GetAvalibleByDomainUsers(company.Domains);
            return View(new CompanyAjax(company));
        }

        public JsonResult Save(Company data)
        {
            var id = companyService.Update(data);
            return this.Success(id);
        }

        public JsonResult AddManager(int cId, int mId)
        {
            companyService.AddManager(cId, mId);
            return this.Success();
        }

        public JsonResult RemoveManager(int cId, int mId)
        {
            companyService.RemoveManager(cId, mId);
            return this.Success();
        }

        public JsonResult GetDomainUsers(string domains)
        {
            return this.Success(GetAvalibleByDomainUsers(domains));
        }

        private IEnumerable<UserAjax> GetAvalibleByDomainUsers(string domains)
        {
            return userService.GetAvalibleByDomain(domains).ToList().Select(x => new UserAjax(x));
        }
    }
}