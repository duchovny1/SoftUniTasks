using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp10
{
    class Program
    {
        static void Main(string[] args)
        {

            string regex = @"[+]359([ -])2\1\d{3}\1\d{4}\b";
            string text = Console.ReadLine();

            MatchCollection match = Regex.Matches(text, regex);
            List<string> phones = new List<string>();

            foreach (Match name in match)
            {

                phones.Add(name.Value);
            }

            Console.Write(string.Join(", ", phones));
        }
    }
}
