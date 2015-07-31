using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelComputingDemo
{
    class DemoApplication
    {
        static void Main()
        {
            Console.WriteLine("--- CPU parallel processing test ---\n");
            Console.WriteLine("Enter number to search all amicable numbers in: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Task parallelTask = new Task(delegate
            {
                var res = Computation.GetAmicableNumbersParallelized(n);
            });
            parallelTask.Start();
            parallelTask.Wait();
            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}.{2:00}",
                ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("Runtime of parallel computation: {0} minutes.\n",
                elapsedTime);

            stopWatch.Reset();
            stopWatch.Start();

            Task synchronizedTask = new Task(delegate
            {
                var res = Computation.GetAmicableNumbersSynchronized(n);
            });
            synchronizedTask.Start();
            synchronizedTask.Wait();
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}.{2:00}",
                ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
      
            Console.WriteLine("Runtime of synchronous computation: {0} minutes.\n",
                elapsedTime);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
