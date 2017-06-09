using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMail.Interfaces
{
    public interface IMailStrategyFactory
    {
        IMailStrategy GetMailStrategy(bool isSecure = false);
    }
}
