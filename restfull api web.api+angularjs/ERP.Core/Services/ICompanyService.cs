using System.Collections.Generic;
using ERP.Core.Models;

namespace ERP.Core.Services
{
    public interface ICompanyService : ICrudService<Company>
    {
        void AddManager(int cId, int mId);
        void RemoveManager(int cId, int mId);
        IEnumerable<Company> GetUserCompanies(int userId);
    }
}