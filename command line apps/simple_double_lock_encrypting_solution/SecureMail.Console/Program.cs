using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SecureMail.Console.DIConfiguration;
using SecureMail.Factories;
using SecureMail.Interfaces;
using SecureMail.Models;
using SecureMail.Strategies;

namespace SecureMail.Console
{
    public static class MyExtension
    {
        public static string GetKey(this IMailStrategyFactory factory)
        {
            return Guid.NewGuid().ToString();
        }
        public static Lock CreateLock(this IMailStrategyFactory factory, string key)
        {
            return new Lock(key);
        }
        public static void MakeLock(this Addressee adr, IMailStrategyFactory factory)
        {
            adr.Key = factory.GetKey();
            adr.MyLock = factory.CreateLock(adr.Key);
        }
    }

    class ProgramEntry
    {
        static void Main(string[] args)
        {
            var container = UnityActivator.ConfigureBindings();
            var program = container.Resolve<ProgramStarter>();

            //this is fo you to try not secure
            System.Console.WriteLine("Try not secure send.");
            program.RunSendProcess(false);

            //this is for you to check how your algorithm works
            System.Console.WriteLine("Try yours secure send.");
            program.RunSendProcess(true);

            System.Console.ReadLine();
        }
    }

    public class ProgramStarter
    {
        private readonly IMailStrategyFactory _mailStrategyFactory;
        public ProgramStarter(IMailStrategyFactory mailStrategyFactory)
        {
            _mailStrategyFactory = mailStrategyFactory;
        }

        public void RunSendProcess(bool useSecureSend = false, int iters = 2)
        {
             
            var p1 = new Addressee("Romeo", new MailBox());
            p1.MakeLock(_mailStrategyFactory);//ma solution
            
            var p2 = new Addressee("Juliette");
            p2.MakeLock(_mailStrategyFactory);//ma solution
            
            var postman = new Postman("Mercutio");
            //Lock v = _mailStrategyFactory.method1();

            for (var i = 0; i<iters; i++)
            {
                try
                {
                    System.Console.Write($"{p1.Name}, please write a message:");
                    string message = System.Console.ReadLine();
                    _mailStrategyFactory.GetMailStrategy(useSecureSend).Send(p1, p2, postman, message);
                    var receivedMessage = p2.GetMailboxMessage();
                    System.Console.WriteLine($"{p2.Name} sucessfully received message:{receivedMessage}");
                    var temp1 = p1;
                    p1 = p2;
                    p2 = temp1;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    break;
                }
            }
        }
    }
}
