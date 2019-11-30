using System;
using System.Collections.Generic;
using System.Text;

namespace Rage_Quit
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            string eachWord = "";


            List<char> uniqueSymbols = new List<char>();

            foreach (var item in input)
            {
                if (!uniqueSymbols.Contains(Char.ToUpper(item)) && !Char.IsDigit(item))
                {
                    uniqueSymbols.Add(Char.ToUpper(item));
                }
            }
            Console.WriteLine($"Unique symbols used: {uniqueSymbols.Count}");

            for (int i = 0; i < input.Length; i++) // 
            {
                if (Char.IsDigit(input[i]))
                {
                    if (Char.IsDigit(input[i + 1]))
                    {
                        string count1 = "input[i]" + "input[i + 1]";
                        int count2 = int.Parse(count1);
                        for (int j = 0; j < count2; j++) //asd3
                        {
                            for (int k = 0; k < eachWord.Length; k++)
                            {
                                Console.Write(Char.ToUpper(eachWord[k]));
                            }
                        }
                        i++;
                        continue;

                    }
                    int count = int.Parse(input[i].ToString());
                    
                    for (int j = 0; j < count; j++) //asd3
                    {
                        for (int k = 0; k < eachWord.Length; k++)
                        {
                            Console.Write(Char.ToUpper(eachWord[k]));
                        }
                    }
                    eachWord = "";
                    continue;
                }

                eachWord += (Char.ToUpper(input[i]));
                
            }

            
           
            Console.WriteLine();
        }
    }
}
