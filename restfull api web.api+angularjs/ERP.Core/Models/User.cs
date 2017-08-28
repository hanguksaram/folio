using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ERP.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User:BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Salt { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsSa { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        [NotMapped]
        public string Name
        {
            get { return string.Format("{0} {1}", this.FirstName, this.LastName); }
        }
    }
}
