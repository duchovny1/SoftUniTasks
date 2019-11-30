using System;
using System.Text;

namespace Replacing_Repeating_Chars
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (i == input.Length - 1)
                {
                    if(input[i] != input[i -1])
                    {
                        output.Append(input[i]);
                    }
                    continue;
                }

                if (i == 0)
                {
                    output.Append(input[i]);
                    continue;
                }
                else if (input[i] == input[i-1]) //aaaaabbbbbcdddeeeedssaa
                {
                    continue;
                }
                output.Append(input[i]);
               


            }

            Console.WriteLine(output);
        }
    }
}
