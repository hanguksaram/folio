using ERP.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Models
{
    public class EmployeeToSkill : BaseEntity
    {
        [Required, ForeignKey("Skill")]
        public int SkillId { get; set; }
        public virtual Skill Skill { get; set; }
        public Preference Preference { get; set; }
        [Required]
        public Level Level { get; set; }
    }
}
