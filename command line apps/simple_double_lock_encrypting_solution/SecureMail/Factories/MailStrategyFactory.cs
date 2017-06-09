using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureMail.Interfaces;
using SecureMail.Strategies;

namespace SecureMail.Factories
{
    public class MailStrategyFactory : IMailStrategyFactory
    {
        public IMailStrategy GetMailStrategy(bool isSecure = false)
        {
            if (!isSecure)
            {
                return new NotSecureMailStrategy();
            }

            return new SecureMailStrategy();
        }
    }
}
