using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zaybu.Compare;

namespace ERP.Core.HistoryStuff
{
    public class HsCertificatePoco
    {
        [KeyProperty]
        public int ID { get; set; }
        public string CertificateName { get; set; }
        public string Comment { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool? Pending { get; set; }
        public override string ToString()
        {
            return String.Format("#{0} {1}", ID, CertificateName);
        }
    }
}