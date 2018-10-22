using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1
{
    class Mutex
    {
        private int ID;

        public Mutex()
        {
            this.ID = 0;
        }

        public void Lock()
        {
            int currentID = Thread.CurrentThread.ManagedThreadId;
            while (Interlocked.CompareExchange(ref ID, currentID, 0) != 0)
            {
                Thread.Sleep(10);
            }
        }

        public void Unlock()
        {
            int currentID = Thread.CurrentThread.ManagedThreadId;
            Interlocked.CompareExchange(ref ID, 0, currentID);
        }
    }
}
