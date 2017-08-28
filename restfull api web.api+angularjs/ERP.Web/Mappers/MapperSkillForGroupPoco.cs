using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP.Core.Models;
using ERP.Web.Models;
using Omu.ValueInjecter;

namespace ERP.Web.Mappers
{
    public static class MapperSkillForGroupPoco
    {
        public static SkillPoco MapToSkillForGroupPoco(this Skill skill)
        {
            return new SkillPoco().InjectFrom(skill) as SkillPoco;
        }

        public static Skill MapToSkill(this SkillPoco skillPoco)
        {
            return new Skill().InjectFrom(skillPoco) as Skill;
        }
    }
}