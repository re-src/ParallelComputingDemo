using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelComputingDemo
{
    class Computation
    {
        public static ConcurrentDictionary<int, int> GetAmicableNumbersParallelized(int n)
        {
            ConcurrentDictionary<int, int> amicableNumbers =
                new ConcurrentDictionary<int, int>();

            Parallel.For(1, n, (i) =>
            {
                Parallel.For(i, n, (j) =>
                {
                    if (i != j &&
                        i == GetEvenDivisors(j).Sum() &&
                        j == GetEvenDivisors(i).Sum())
                    {
                        amicableNumbers.TryAdd(i, j);
                        Console.WriteLine("Found {0} and {1} using Thread {2}.",
                            GetEvenDivisors(i).Sum(),
                            GetEvenDivisors(j).Sum(),
                            Thread.CurrentThread.ManagedThreadId);
                    }
                }
                );
            });

            return amicableNumbers;
        }

        public static Dictionary<int, int> GetAmicableNumbersSynchronized(int n)
        {
            Dictionary<int, int> amicableNumbers =
                new Dictionary<int, int>();

            for (int i = 1; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    if (i != j &&
                        i == GetEvenDivisors(j).Sum() &&
                        j == GetEvenDivisors(i).Sum())
                    {
                        amicableNumbers.Add(i, j);
                        Console.WriteLine("Found {0} and {1} using Thread {2}.",
                            GetEvenDivisors(i).Sum(),
                            GetEvenDivisors(j).Sum(),
                            Thread.CurrentThread.ManagedThreadId);
                    }
                }
            }
            return amicableNumbers;
        }

        private static List<int> GetEvenDivisors(int n)
        {
            List<int> divisors = new List<int>();
            for (int i = 1; i < n; i++)
            {
                if (n % i == 0)
                {
                    divisors.Add(i);
                }
            }
            return divisors;
        }

    }
}
