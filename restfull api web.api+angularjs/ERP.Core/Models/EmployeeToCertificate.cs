using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Models
{
    public class EmployeeToCertificate : BaseEntity
    {
        [Required, ForeignKey("Certificate")]
        public int CertificateId { get; set; }
        public virtual Certificate Certificate { get; set; }
        public string Comment { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool? Pending { get; set; }
    }
}
