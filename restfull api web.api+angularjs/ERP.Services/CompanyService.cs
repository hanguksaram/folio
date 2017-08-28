using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Core.Models;
using ERP.Core.Repository;
using ERP.Core.Services;

namespace ERP.Services
{
    public class CompanyService: CrudService<Company>, ICompanyService
    {
        public CompanyService(IRepo<Company> repo) : base(repo)
        {
        }

        public void AddManager(int cId, int mId)
        {
            var company = this.Get(cId);
            var ls = new User { ID = mId};
            this.Repo.Attach(ls);
            company.Managers.Add(ls);                
            this.Repo.Save();            
        }

        public void RemoveManager(int cId, int mId)
        {
            var company = this.Get(cId);
            company.Managers = company.Managers.Where(x => x.ID != mId).ToList();
            Repo.Save();
        }

        public IEnumerable<Company> GetUserCompanies(int userId)
        {
            return this.Repo.Where(x => x.Managers.Any(m => m.ID == userId));
        }
    }
}
