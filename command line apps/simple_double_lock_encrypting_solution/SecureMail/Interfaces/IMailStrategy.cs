using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureMail.Models;

namespace SecureMail.Interfaces
{
    public interface IMailStrategy
    {
        void Send(Addressee sender, Addressee receiver, Postman postman, string message);
    }
}
