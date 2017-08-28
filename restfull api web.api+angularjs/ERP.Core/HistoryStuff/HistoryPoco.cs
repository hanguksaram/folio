using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERP.Core.HistoryStuff
{
    public class HistoryPoco
    {
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PositionName { get; set; }
        public DateTime? FiredDate { get; set; }
        public ICollection<HsPropertyPoco> Properties { get; set; }
        public ICollection<HsSkillPoco> Skills { get; set; }
        public ICollection<HsCertificatePoco> Certificates { get; set; }
    }
}
