using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Models
{
    public class Property : BaseEntity
    {
        [Required]
        public string PropertyValue { get; set; }
        [Required, ForeignKey("PropertyType")]
        public int PropertyTypeId { get; set; }
        public virtual PropertyType PropertyType { get; set; }
    }
}
