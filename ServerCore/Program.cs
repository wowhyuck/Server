using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Lock
    {
        // bool <- 커널
        //AutoResetEvent _available = new AutoResetEvent(true);         // 톨게이트, 자동 여닫이
        ManualResetEvent _available = new ManualResetEvent(true);       // 문, 수동

        public void Acquire()
        {
            // AutoResetEvent
            _available.WaitOne();       // 입장 시도

            // ManualResetEvent -> 밑에 코드 경우 원자성 코드가 아니므로 잘못된 결과가 나온다. 
            //                     이 경우는 AutoRestEvent 쓰는것이 좋다
            //_available.WaitOne();       // 입장 시도
            //_available.Reset();         // 문을 닫는다
        }

        public void Release()
        {
            _available.Set();           // flag = true
        }
    }

    class Program
    {
        static int _num = 0;
        static Lock _lock = new Lock();

        static void Thread_1()
        {
            for (int i = 0; i < 10000; i++)
            {
                _lock.Acquire();
                _num++;
                _lock.Release();
            }
        }

        static void Thread_2()
        {
            for (int i = 0; i < 10000; i++)
            {
                _lock.Acquire();
                _num--;
                _lock.Release();
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);
            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(_num);
        }
    }
}
