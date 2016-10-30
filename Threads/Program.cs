using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    class Program
    {

        public static void LongOperation(string taskName)
        {
            Thread.Sleep(1000);
            Console.WriteLine("{0} Finished . Executing Thread : {1}", taskName,
            Thread.CurrentThread.ManagedThreadId);
        }

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Parallel.Invoke(() => LongOperation("A"),
            () => LongOperation("B"),
            () => LongOperation("C"),
            () => LongOperation("D"),
            () => LongOperation("E"));
            stopwatch.Stop();
            Console.WriteLine("Parallel long operation calls finished {0} sec.",
            stopwatch.Elapsed.TotalSeconds);

            stopwatch.Restart();
            Parallel.For(0, 1000, (i) =>
            {
                var x = 2;
                var y = 2;
                var sum = x + y;
            });
            stopwatch.Stop();
            Console.WriteLine("Parallel calls finished {0} ms.",
            stopwatch.Elapsed.TotalMilliseconds);
            stopwatch.Restart();
            for (int i = 0; i < 1000; i++)
            {
                int x = 2;
                int y = 2;
                int sum = x + y;
            }
            stopwatch.Stop();
            Console.WriteLine("Sync operation calls finished {0} ms.",
            stopwatch.Elapsed.TotalMilliseconds);

            int counter = 0;
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                counter += 1;
            });
            Console.WriteLine("Counter should be 100. Counter is {0}", counter);
            counter = 0;
            object objectUsedForLock = new object();
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                lock (objectUsedForLock)
                {
                    counter += 1;
                }
            });
            Console.WriteLine("Counter should be 100. Counter is {0}", counter);

            List<int> results = new List<int>();
            object locker = new object();
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                lock (locker)
                {
                    results.Add(i * i);
                }
            });
            Console.WriteLine("Bag length should be 100. Length is {0}", results.Count);
        }
    }
}
