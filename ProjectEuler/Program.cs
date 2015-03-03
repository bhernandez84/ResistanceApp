using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    class Program
    {

        static void Main(string[] args)
        {

            Stopwatch timePerParse = Stopwatch.StartNew();

            //10001 prime number
            int primeNumber = 2;
            int primeCounter = 1;
            int targetPrime = 10001;
            while (primeCounter < targetPrime)
            {
                primeNumber++;
                if (primeNumber.isPrime())
                {
                    primeCounter++;
                }

            }
            Console.WriteLine(primeNumber);
                Console.WriteLine(timePerParse.Elapsed);
            Console.ReadLine();
            
        }


    }

}
