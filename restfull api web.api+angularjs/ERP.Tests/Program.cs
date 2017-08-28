using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Core.Models;
using ERP.Services;
using ERP.SqlServer;
using ERP.Synchronizer.Jira;

namespace ERP.Tests
{
    class Program
    {
        static void Main()
        {
            var repo = new Repo<Report>(new DbContextFactory(), new SynAuthProvider());
            var wlrepo = new Repo<Agreement>(new DbContextFactory(), new SynAuthProvider());
            var rprepo = new Repo<ReportProgress>(new DbContextFactory(), new SynAuthProvider());
            var srv = new ReportService(repo, wlrepo, rprepo);
            var reports = srv.GetAll().ToList();
            foreach (var report in reports)
            {
                Console.WriteLine("Update: " + report.Name);
                try
                {
                    srv.Update(report);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error on: " + report.Name);
                 //   Console.ReadKey();
                }
                
            }   
            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
