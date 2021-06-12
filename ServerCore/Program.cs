using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    // 메모리 베리어
    // A. 코드 재배치 억제
    // B. 가시성

    // 1) Full Memory Barrier (ASM MFENCE, C# Thread.MemoryBarrier) : Store/Load 둘다 막는다
    // 2) Store Memry Barrier (ASM SFENCE) : Store만 막는다
    // 3) Load Memry Barrier (ASM LFENCE) : Load만 막는다

    class Program
    {
        static int number = 0;

        // atomic = 원자성
        static void Thread_1()
        {
            for (int i = 0; i < 10000; i++)
            {   // 어셈블리어에선 3단계로 이루어짐 = number++
                //number++;

                //int temp = number;  // 0
                //temp += 1;          // 1
                //number = temp;      // number = 1

                // All or Nothing
                Interlocked.Increment(ref number);
            }
        }

        static void Thread_2()
        {
            for (int i = 0; i < 10000; i++)
            {
                //number--;

                //int temp = number;  // 0
                //temp -= 1;          // -1
                //number = temp;      // number = -1

                Interlocked.Decrement(ref number);
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);
            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(number);
        }
    }
}
