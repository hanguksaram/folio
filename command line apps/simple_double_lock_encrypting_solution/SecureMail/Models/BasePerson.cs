using System.Collections.Specialized;

namespace SecureMail.Models
{
    public abstract class BasePerson
    {
        protected MailBox MailBox;
        public MailBox MMailBox//my solution
        {
            set { MailBox = value; }
            get { return MailBox; }
        }

        public string Name { get; private set; }

        protected BasePerson(string name, MailBox mb = null)
        {
            Name = name;
            MailBox = mb;
        }
        //protected void ReceiveMail(MailBox box)
        //{

        //}
        //my solution
        public virtual void GivetMailBox(BasePerson person)
        {   
            if (this.MailBox != null)
            {
                person.MailBox = this.MailBox;
                this.MailBox = null;
            }
                
                
        }
    }
}
