using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace test_text_processing
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();


            while (true)
            {
                StringBuilder decryptedCode = new StringBuilder();
                string regexPattern = @"([#$%*&])(?<name>[A-Za-z]+)\1=(?<length>[0-9]+)[!]{2}(?<codeToDecrypt>.+)";
                bool isValid = Regex.IsMatch(input, regexPattern);
                string name = "";
                int length = 0;
                string codeToDecrypt = "";

                if (isValid)
                {
                    MatchCollection matches = Regex.Matches(input, regexPattern);

                    foreach (Match item in matches)
                    {
                        name = item.Groups["name"].Value;
                        length = int.Parse(item.Groups["length"].Value);
                        codeToDecrypt = item.Groups["codeToDecrypt"].Value;
                    }

                    if (length == codeToDecrypt.Length)
                    {
                        for (int i = 0; i < codeToDecrypt.Length; i++)
                        {
                            int positionOfNewChar = (int)codeToDecrypt[i] + length;
                            char newChar = (char)positionOfNewChar;

                            decryptedCode.Append(newChar);
                        }

                        Console.WriteLine($"Coordinates found! {name} -> {decryptedCode}");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Nothing found!");
                    }
                }
                else
                {
                    Console.WriteLine($"Nothing found!");
                }
                input = Console.ReadLine();
            }

        }
    }
}
