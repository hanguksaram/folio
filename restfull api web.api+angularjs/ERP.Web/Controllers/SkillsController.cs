using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Core.Models;
using ERP.Core.Services;
using ERP.Web.Models;
using Omu.ValueInjecter;


namespace ERP.Web.Controllers
{   //TODO: is it neccesary to pass id field in skill entity to client or not7, depends on filter's model implemended in view
    [RoutePrefix("api/skills")]
    public class SkillsController : BaseController
    {

        //field for adding process testing


        private readonly ICrudService<Skill> skillService;

        //injectable type was changed in unity.config
        public SkillsController(ICrudService<Skill> skillService)
        {

            this.skillService = skillService;

        }

        
        [HttpGet, Route("")]
        public JsonResult GetAllSkills()
        {
            try
            {
                var skills = skillService.GetAll().Select
                    (s => new EmployeeToSkillPoco()
                    {SkillName = s.SkillName, ID = s.ID}).ToList();
                return Success(skills);

            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }


        [HttpPost, Route("")]
        public JsonResult CreateNewSkill(EmployeeToSkillPoco skill)
        {
            if (!ModelState.IsValid)
            {
                var modelStatePoco = ModelStatePoco.MapFromModelState(ModelState);
                return Error(modelStatePoco);
            }

            try
            {
                skillService.Update(new Skill() {SkillName = skill.SkillName, ID = skill.ID});
                return Success();
            }

            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
