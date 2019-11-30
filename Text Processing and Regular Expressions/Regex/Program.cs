using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Regex
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] usernames = Console.ReadLine().Split(", ");

              List<string> result = new List<string>();

            bool length = false;
            bool contains = false;
            bool symbols = false;
            

            for (int i = 0; i < usernames.Length; i++)
            {
                if (usernames[i].Length <= 16 && usernames[i].Length >= 3)
                {
                    length = true;
                }
                if (Regex.IsMatch())
                { }
                Reg
            }


        }
    }
}
