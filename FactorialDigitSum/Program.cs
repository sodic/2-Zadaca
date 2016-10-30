using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorialDigitSum
{
    class Program
    {
        static async void SimulateAsync(int n)
        {
            Console.WriteLine(await FactorialDigitSumAsyinc(n));
        }

        static async Task<int> FactorialDigitSumAsyinc(int n)
        {
            int factorial = 1;
            for (int i = 2; i <= n; i++)
                factorial *= i;
            int sum = 0;
            for (; factorial != 0; factorial /= 10)
                sum += factorial % 10;
            return sum;
        }
        static void Main(string[] args)
        {
            SimulateAsync(6);
        }
    }
}
