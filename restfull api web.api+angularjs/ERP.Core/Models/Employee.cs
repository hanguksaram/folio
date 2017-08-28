using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Models
{
    public class Employee:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required, ForeignKey("Position")]
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public DateTime HiringDate { get; set; }
        public DateTime? FiredDate { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<EmployeeToSkill> Skills { get; set; }
        public virtual ICollection<EmployeeToCertificate> Certificates { get; set; }
        public virtual ICollection<HistoryEmployee> Histories { get; set; }
    }
}
