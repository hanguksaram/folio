using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SecureMail.Factories;
using SecureMail.Interfaces;

namespace SecureMail.Console.DIConfiguration
{

   

    public static class UnityActivator
    {
        public static IUnityContainer ConfigureBindings()
        {
            var container = new UnityContainer();
            container.RegisterType<ProgramStarter, ProgramStarter>();
            container.RegisterType<IMailStrategyFactory, MailStrategyFactory>();
            return container;
        }
    }
}
