using ERP.Core.Models;
using ERP.Web.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Mappers
{
    public static class MapperGroupOfSkillsPoco
    { 
        public static GroupOfSkillsPoco MapToGroupOfSkillsPoco(this GroupOfSkills groupOfSkills)
        {
            var groupOfSkillsPoco = new GroupOfSkillsPoco();

            groupOfSkillsPoco.InjectFrom(groupOfSkills);
           
            if(groupOfSkills.Skills != null)
            {
                var skillsPoco = new List<SkillPoco>();
                foreach (var oneSkill in groupOfSkills.Skills)
                {
                    skillsPoco.Add(oneSkill.MapToSkillForGroupPoco());
                }

                groupOfSkillsPoco.Skills = skillsPoco;
            }
            
            return groupOfSkillsPoco;
        }

        public static GroupOfSkills MapToGroupOfSkills(this GroupOfSkillsPoco groupOfSkillsPoco)
        {
            var groupOfSkills = new GroupOfSkills();

            groupOfSkills.InjectFrom(groupOfSkillsPoco);

            
            if (groupOfSkillsPoco.Skills != null)
            {
                var skills = new List<Skill>();
                foreach (var oneSkillPoco in groupOfSkillsPoco.Skills)
                {
                    skills.Add(oneSkillPoco.MapToSkill());
                }

                groupOfSkills.Skills = skills;
            }
          
            return groupOfSkills;
        }
    }
}