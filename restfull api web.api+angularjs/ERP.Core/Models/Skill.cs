using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Models
{
    public class Skill : BaseEntity
    {
        [Required]
        public string SkillName { get; set; }
        public virtual ICollection<GroupOfSkills> Groups { get; set; }
    }
}
