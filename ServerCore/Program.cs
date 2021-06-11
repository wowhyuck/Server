using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        static void MainThread(object state)
        {
            Console.WriteLine("Hellow Thread!");
        }

        static void Main(string[] args)
        {

            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(5, 5);

            for (int i = 0; i < 4; i++)
            {
                Task t = new Task(() => { while (true) { } }, TaskCreationOptions.LongRunning);
                t.Start();
            }

            //for (int i = 0; i < 4; i++)
            //    ThreadPool.QueueUserWorkItem((obj) => { while (true) { } });

            ThreadPool.QueueUserWorkItem(MainThread);       // Background처럼 Main이 끝나면 Thread도 종료

            //Thread t = new Thread(MainThread);
            //t.Name = "Test Thread";
            //t.IsBackground = true;      // Main이 끝나면 해당 Thread도 종료
            //t.Start();
            //Console.WriteLine("Waiting for Thread");

            //t.Join();
            //Console.WriteLine("Hello World!");

            while(true)
            {

            }
        }
    }
}
