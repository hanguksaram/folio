using ERP.Core.Services;
using ERP.Core.Models;
using ERP.Web.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Web.Models;
using Omu.ValueInjecter;

namespace ERP.Web.Controllers
{
    [RoutePrefix("api/groupsofskills")]
    public class GroupsOfSkillsController : BaseController
    {
        private readonly ICrudService<GroupOfSkills> _groupsOfSkillsService;

        public GroupsOfSkillsController(IGroupOfSkillsService groupsOfSkillsService)
        {
            this._groupsOfSkillsService = groupsOfSkillsService;
        }

        [HttpGet, Route("")]
        public JsonResult GetAllGroupsOfSkills()
        {
            return Success(_groupsOfSkillsService.GetAll().Select(g => g.MapToGroupOfSkillsPoco()));
        }

        [HttpGet, Route("{id}")]
        public ActionResult GetOneGroupOfSkills(int id)
        {
            var group = _groupsOfSkillsService.Get(id);
            if (group == null)
                throw new HttpException(404, "Group og skills not found.");
            return Success(group.MapToGroupOfSkillsPoco());
        }

        [HttpPost, Route("")]
        public ActionResult PostOneGroupOfSkills(GroupOfSkillsPoco groupOfSkillsPoco)
        {
            groupOfSkillsPoco.ID = 0;
            if (ModelState.IsValid)
                return Success(_groupsOfSkillsService.Update(groupOfSkillsPoco.MapToGroupOfSkills()));
            else
                return Error(ModelStatePoco.MapFromModelState(ModelState));
        }

        [HttpPut, Route("{id}")]
        public ActionResult PutOneGroupOfSkills(int id, GroupOfSkillsPoco groupOfSkillsPoco)
        {
            var group = _groupsOfSkillsService.Get(id);
            if (group == null)
                throw new HttpException(404, "Group of skills not found.");
            if (ModelState.IsValid)
                return Success(_groupsOfSkillsService.Update(groupOfSkillsPoco.MapToGroupOfSkills()));
            else
                return Error(ModelStatePoco.MapFromModelState(ModelState));
        }

        [HttpDelete, Route("{id}")]
        public ActionResult DeleteOneGroupOfSkills(int id)
        {
            _groupsOfSkillsService.Delete(id);
            return Success();
        }
    }
}