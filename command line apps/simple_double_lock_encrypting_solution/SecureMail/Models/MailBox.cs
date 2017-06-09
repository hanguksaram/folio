using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SecureMail.Models;

namespace SecureMail
{
    /// <summary>
    /// Шкатулка с возможностью навешивать замки (locks)
    /// </summary>
    public class MailBox
    {
        private readonly IList<Lock> _locks;
        private string _message;

        public MailBox()
        {
            _locks=new List<Lock>();
        }

        public string Message {
            get
            {
                if (!IsOpen)
                {
                    throw new InvalidOperationException("Could not read message, mailBox is closed");
                }

                return _message;
            }
            set
            {
                if (!IsOpen)
                {
                    throw new InvalidOperationException("Could not put message, mailBox is closed");
                }

                _message = value;
            }
        }

        public bool IsOpen
        {
            get { return _locks.All(t => t.IsOpen); }
        }

        public void AddLock(Lock @lock)
        {
            if (!@lock.IsOpen)
            {
                throw new InvalidOperationException("Cannot add lock to mailbox because lock is closed");
            }
            _locks.Add(@lock);
        }

        public IList<Lock> GetLocks()
        {
            return _locks.ToList();
        }
    }
}
