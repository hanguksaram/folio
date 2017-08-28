using ERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Core.Repository;
using Omu.ValueInjecter;
using ERP.Core;
using ERP.Core.Services;

namespace ERP.Services
{
    public class GroupOfSkillsService : CrudService<GroupOfSkills>, IGroupOfSkillsService
    {
        public GroupOfSkillsService(IRepo<GroupOfSkills> repo) : base(repo)
        { }

        public override int Update(GroupOfSkills group)
        {
            if (group.ID == 0)
            {
                var newGroup = this.Repo.Insert(group);
                this.Repo.Save();
                return newGroup.ID;
            }

            var updatedGroup = this.Get(group.ID);
            if (updatedGroup == null)
                return 0;
            this.UpdateSkills(updatedGroup, group.Skills);
            this.Repo.Save();
            return group.ID;
        }

        private void UpdateSkills(GroupOfSkills updatedGroup, ICollection<Skill> skillsFrom)
        {
            // add new skill
            foreach (var skillFrom in skillsFrom)
            {
                if (skillFrom.ID == 0)
                {
                    var isExist = false;
                    foreach (var skillTo in updatedGroup.Skills)
                    {
                        if (String.Compare(skillTo.SkillName, skillFrom.SkillName, true) == 0)
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                        updatedGroup.Skills.Add(skillFrom);
                }
            }
        }
    }
}
