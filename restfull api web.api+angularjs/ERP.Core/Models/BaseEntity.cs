using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity
    {
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime ModifyDate { get; set; }
        [Required]
        public int ID { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string ModifyBy { get; set; }
    }
}
