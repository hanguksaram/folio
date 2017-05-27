using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace palindromapps
{
    class Program
    {
        private static string getPalindrome(string inputtedString)
        {
            int firstI = 0, secondI = 0;
            string requiaredPalindrome = "";
            string palindrome;
            
            for (int i = 1; i < inputtedString.Length - 1; i++)
            {
                secondI = i - 1;
                firstI = i + 1;
                while (secondI >= 0 && firstI < inputtedString.Length)
                {
                    if (inputtedString[secondI] != inputtedString[firstI])
                    {
                        break;
                    }
                    palindrome = inputtedString.Substring(secondI, firstI - secondI + 1);
                    if (palindrome.Length > requiaredPalindrome.Length)
                        requiaredPalindrome = palindrome;
                    secondI--;
                    firstI++;
                }
            }
            return requiaredPalindrome;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Input string");
            Console.WriteLine(getPalindrome(Console.ReadLine()));
            Console.ReadKey();


            

            

            

        }

    }
}
