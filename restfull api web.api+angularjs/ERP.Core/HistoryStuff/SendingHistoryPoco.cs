using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Core.HistoryStuff
{
    public class SendingHistoryPoco
    {
        public DateTime ModifyDate { get; set; }
        public string Changes { get; set; }
        public string Performer { get; set; }
    }
}