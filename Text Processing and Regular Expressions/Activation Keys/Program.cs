using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Activation_Keys
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split("&");

            List<string> CDKEYS = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                StringBuilder activationKey = new StringBuilder(); // 

                if (input[i].Length != 16 && input[i].Length != 25)
                {
                    continue;
                }

                bool isValid = Regex.IsMatch(input[i], @"^[A-Za-z0-9]+$");

                if (isValid == false)
                {
                    continue;
                }

                string eachKey = input[i];
                for (int j = 0; j < eachKey.Length; j++) /// a s d f 4 5 2 
                {
                    char currentChar = eachKey[j];
                    if (Char.IsLetter(currentChar))
                    {
                        activationKey.Append(char.ToUpper(currentChar));

                    }
                    else if (Char.IsNumber(currentChar))
                    {
                        int number = currentChar - '0';
                        int finalNum = Math.Abs(9 - number);
                        activationKey.Append(finalNum);
                    }

                }

                if (activationKey.Length == 16)
                {
                    activationKey.Insert(4, '-');
                    activationKey.Insert(9, '-');
                    activationKey.Insert(14, '-');
                }
                else if (activationKey.Length == 25)
                {
                    activationKey.Insert(5, '-');
                    activationKey.Insert(11, '-');
                    activationKey.Insert(17, '-');
                    activationKey.Insert(23, '-');
                }

                CDKEYS.Add(activationKey.ToString());
            }
            
            Console.Write(string.Join(", ", CDKEYS));
            Console.WriteLine();
           
        }
    }
}
