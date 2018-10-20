using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab1
{
    class TaskQueue : IEnumerable, IDisposable
    {
        private Thread[] threadPool;
        private int ThreadsNum = 0;
        public delegate void taskDelegate();
        Mutex mutex = new Mutex();
        private Queue<taskDelegate> TaskDelegateQueue = new Queue<taskDelegate>();
        public TaskQueue(int ThreadsNum)
        {
            threadPool = new Thread[ThreadsNum];
            this.ThreadsNum = ThreadsNum;
            for (int n = 0; n < ThreadsNum; n++)
            {
                threadPool[n] = new Thread(CompletingTasks) { IsBackground = true };
                threadPool[n].Name = "Thread " + (n + 1).ToString();
                threadPool[n].Start();
            }

        }

        public void EnqueueTask(taskDelegate task)
        {
            TaskDelegateQueue.Enqueue(task);
        }

        void CompletingTasks()
        {
            taskDelegate task = null;
            while (true)
            {
                if (TaskDelegateQueue.Count > 0)
                {
                    //SpinWait!!
                    lock (TaskDelegateQueue)
                    {
                        if (TaskDelegateQueue.Count > 0)
                            task = TaskDelegateQueue.Dequeue();
                    }
                    Console.WriteLine("{0} start task", Thread.CurrentThread.Name);
                    task.Invoke();
                    Console.WriteLine("{0} complete task", Thread.CurrentThread.Name);
                }
            }
        }

        public bool AreTasksCompleted()
        {
            lock(TaskDelegateQueue)
                return TaskDelegateQueue.Count == 0;
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < ThreadsNum; i++)
            {
                yield return threadPool[i];
            }
        }
        private bool _disposed;
        private readonly object _disposeLock = new object();

        protected virtual void Dispose(bool disposing)
        {
            lock (_disposeLock)
            {
                if (!_disposed)
                {
                    for (int i = 0; i < ThreadsNum; i++)
                        threadPool[i].Interrupt();

                    _disposed = true;
                    if (disposing) GC.SuppressFinalize(this);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~TaskQueue()
        {
            Dispose(false);
        }
    }
}
