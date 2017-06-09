using SecureMail.Interfaces;
namespace SecureMail.Models
{
    public class Lock
    {
        private readonly string key;
        public Lock(string key)
        {
            this.key = key;
        }

        private bool isOpen;
        public bool IsOpen => isOpen;
        public void Open(string key)
        {
            if (isOpen)
                return;
            if (this.key == key)
                isOpen = true;
        }

        public void Close(string key)
        {
            if (!isOpen)
                return;
            if (this.key == key)
                isOpen = false;
        }
    }
}
