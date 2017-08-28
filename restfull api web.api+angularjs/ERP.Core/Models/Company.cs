using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ERP.Core.Models
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public int TimeZone { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Domains { get; set; }
        public string Logo { get; set; }
        public string EmailTitle { get; set; }
        public string EmailTemplate { get; set; }
        public virtual ICollection<User> Managers { get; set; } 
    }
}
