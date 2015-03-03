using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public static class Helpers
    {
        public static bool isFactorOf(this int myInt, long numberToCheck)
        {
            return (numberToCheck % myInt) == 0;
        }


        public static bool isPrime(this int number)
        {
            for (int index = 2; index <= Math.Sqrt(number); index++)
                if (number % index == 0)
                {
                    return false;
                }
            return true;
        }

        public static bool isPalindrome(this int number)
        {
            string numberString = number.ToString();

            string reversed = String.Join("",numberString.Reverse());

            if (numberString == reversed)
                return true;
            return false;
        }


    }

}
