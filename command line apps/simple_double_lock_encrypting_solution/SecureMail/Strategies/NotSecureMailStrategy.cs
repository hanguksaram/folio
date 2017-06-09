using SecureMail.Interfaces;
using SecureMail.Models;

namespace SecureMail.Strategies
{
    public class NotSecureMailStrategy : IMailStrategy
    {
        public void Send(Addressee sender, Addressee receiver, Postman postman, string message)
        {
            sender.PutMessage(message);
            sender.GivetMailBox(postman);
            postman.Send(receiver);
        }
    }
}
