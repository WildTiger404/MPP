using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Lab1
{
    static class Parallel
    {
        public static void WaitAll(List<TaskQueue.taskDelegate> tasks)
        {
            TaskQueue taskQueue = new TaskQueue(5);
            bool isRunning = true;

            foreach (var del in tasks)
            {
                taskQueue.EnqueueTask(del);
            }

            while (isRunning)
            {
                if (taskQueue.AreTasksCompleted())
                {
                    Console.WriteLine("All tasks are completed\n\n");
                    isRunning = false;
                }
                Thread.Sleep(300);
            }
        }
    }
}
