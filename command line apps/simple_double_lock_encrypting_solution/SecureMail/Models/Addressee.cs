using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureMail.Interfaces;

namespace SecureMail.Models
{
    public class Addressee : BasePerson
    {
        private Lock _mylock;
        public Lock MyLock//my solution
        {
            set { _mylock = value; }
        }
        private string _key;
        public string Key//my solution
        {
            set { _key = value; }
            get { return _key; }
        }

        public Addressee(string name, MailBox mb = null) : base(name, mb)
        {
            NameCheck();
            //not very OOP style, need to create a factory of locks
            //_key = Guid.NewGuid().ToString();
          
            //i decided to expand existing IMalStrategyFactory adding some extension methods, thus i emulated Lock's factory pattern
            // U can check it in Programm.cs 19-30 lines
        }
        public void Unlock()
        {
            if (this.MailBox == null)
            {
                throw new Exception("Addressee does not have mailbox");
            }

            foreach (var @lock in this.MailBox.GetLocks())
            {
                @lock.Open(_key);
            }
        }

        public void Lock()
        {
            if (this.MailBox == null)
            {
                throw new Exception("Addressee does not have mailbox");
            }

            _mylock.Open(_key);
            this.MailBox.AddLock(_mylock);
            _mylock.Close(_key);
        }

      //  public void GiveMailBoxTo(Postman postman)
      //  {
      //      if (this.MailBox == null)
      //      {
      //          throw new Exception("Addressee does not have mailbox");
      //      }
      //      postman.RecieveMailbox(this.MailBox);
      //      this.MailBox = null;
      //  }

       // public void ReceiveMailBoxFrom(Postman postman)
       // {
       //     this.MailBox = postman.GiveMailBoxTo(this);
       // }

        public string GetMailboxMessage()
        {
            if (this.MailBox == null)
            {
                throw new Exception("Addressee does not have mailbox");
            }

            return this.MailBox.Message;
        }

        public void PutMessage(string message)
        {
            if (this.MailBox == null)
            {
                throw new Exception("Addressee does not have mailbox");
            }

            this.MailBox.Message = message;
        }
        //my solution
        public override void GivetMailBox(BasePerson person)
        {
            if (person is Addressee)
                throw new Exception("U can't send it without postman");

            if (this.MailBox != null)
            {
                person.MMailBox = this.MailBox;
                this.MailBox = null;
            }
        }
        public void NameCheck()//my solution
        {
            if (this.Name != "Romeo" && this.Name != "Juliette")
            {
                throw new Exception("Wrong adressee");
            }
        }
    }
}
