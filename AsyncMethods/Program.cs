using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FactorialDigitSum;

namespace AsyncMethods
{


    class Program
    {

        static async Task<int> FactorialDigitSumAsyinc(int n)
        {
            Thread.Sleep(500);
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
            // Main method is the only method that
            // can ’t be marked with async .
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other .NET application types ( like web apps , win apps etc .)
            // Ignore main method , you can just focus on LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in call hierarchy .
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }
        private static async void LetsSayUserClickedAButtonOnGuiMethod()
        {
            Console.WriteLine(await GetTheMagicNumber());
        }
        private static async Task<int> GetTheMagicNumber()
        {
            return await IKnowAGuyWhoKnowsAGuy();
        }
        private static async Task<int> IKnowAGuyWhoKnowsAGuy()
        {
            return await IKnowWhoKnowsThis(10) + await IKnowWhoKnowsThis(5);
        }
        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            return await FactorialDigitSumAsyinc(n);
        }
    }
}
