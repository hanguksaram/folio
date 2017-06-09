using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureMail.Interfaces;
using SecureMail.Models;

namespace SecureMail.Strategies
{
    public class SecureMailStrategy : IMailStrategy
    {
        public void Send(Addressee sender, Addressee receiver, Postman postman, string message)
        {
            sender.PutMessage(message);
            sender.Lock();
            sender.GivetMailBox(postman);
            //postman.RecieveMailbox();
            postman.Send(receiver);
            
            receiver.Lock();
            receiver.GivetMailBox(postman);
            postman.Send(sender);
            sender.Unlock();
            sender.GivetMailBox(postman);
            postman.Send(receiver);
            receiver.Unlock();
            
            
        }
    }
}