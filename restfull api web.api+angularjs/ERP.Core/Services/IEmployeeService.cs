using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Core.Models;
using ERP.Core.Repository;

namespace ERP.Core.Services
{
    public interface IEmployeeService: ICrudService<Employee>
    {
        IHistoryService historyService { get; }
    }
}
