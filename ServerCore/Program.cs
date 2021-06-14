using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class FastLock
    {
        public int id;
    }

    class SessionManager
    {
        FastLock I;
        static object _lock = new object();

        static public void TestSession()
        {
            lock(_lock)
            {

            }
        }

        static public void Test()
        {
            lock(_lock)
            {
                UserManager.TestUser();
            }
        }
    }

    class UserManager
    {
        FastLock I;
        static object _lock = new object();

        static public void Test()
        {
            lock (_lock)
            {
                SessionManager.TestSession();
            }
        }

        static public void TestUser()
        {
            lock (_lock)
            {

            }
        }
    }

    class Program
    {
        static int number = 0;
        static object _obj = new object();

        static void Thread_1()
        {
            for (int i = 0; i < 100; i++)
            {
                SessionManager.Test();
            }
        }

        static void Thread_2()
        {
            for (int i = 0; i < 100; i++)
            {
                UserManager.Test();
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);
            t1.Start();

            Thread.Sleep(100);

            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(number);
        }
    }
}
