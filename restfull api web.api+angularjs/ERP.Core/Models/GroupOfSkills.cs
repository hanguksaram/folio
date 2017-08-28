using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Models
{
    public class GroupOfSkills : BaseEntity
    {
        [Required]
        public string GroupName { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
    }
}
