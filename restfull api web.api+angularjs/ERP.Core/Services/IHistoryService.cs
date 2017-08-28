using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Core.HistoryStuff;
using ERP.Core.Models;
using ERP.Core.Repository;

namespace ERP.Core.Services
{
    public interface IHistoryService: ICrudService<HistoryEmployee>
    {
        IList<SendingHistoryPoco> GetHistoryById(int Id);
        IList<SendingHistoryPoco> GetShortHistoryById(int Id);
        int Update(int employeeId);
    }
}
