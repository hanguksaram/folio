using System;

namespace SecureMail.Models
{
    /// <summary>
    /// Почтальон
    /// </summary>
    public class Postman : BasePerson
    {
        private Addressee to;

        public void Send(Addressee to)
        {
            if (this.MailBox == null)
            {
                throw new Exception("Postman does not have mailbox");
            }

            if (base.MailBox.IsOpen)
            {
                throw new InvalidOperationException("Mailbox is open! Postman have access to the message");
            }

            this.to = to;
            GivetMailBox(to);
            //to.ReceiveMailBoxFrom(this);
        }

        //seems like this methods should be common for all persons
        //In fight with complexity i solve this problem deleting some redunant methods (imho) and adding common method to base class and it's overriting version in Addressee
       // public void RecieveMailbox(MailBox mb)
       // {
       //     this.MailBox = mb;
       // }

       //public MailBox GiveMailBoxTo(BasePerson receiver)
       //{
       //    if(receiver!=to)
       //        throw new InvalidOperationException("Invalid receiver");
       //    var result = base.MailBox;
       //    base.MailBox = null;
       //    return result;
       //}

        public Postman(string name, MailBox mb = null) : base(name, mb)
        {
        }
    }
}
