using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Models
{
    public class Certificate :  BaseEntity
    {
        [Required]
        public string CertificateName { get; set; }
    }
}
