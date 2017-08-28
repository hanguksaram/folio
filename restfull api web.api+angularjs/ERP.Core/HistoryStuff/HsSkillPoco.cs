using ERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zaybu.Compare;

namespace ERP.Core.HistoryStuff
{
    public class HsSkillPoco
    {
        [KeyProperty]
        public string SkillName { get; set; }
        public int Level { get; set; }
        public string Preference { get; set; }
        public override string ToString()
        {
            return String.Format("{0} | {1} | {2}", SkillName, Level, Preference);
        }
    }
}