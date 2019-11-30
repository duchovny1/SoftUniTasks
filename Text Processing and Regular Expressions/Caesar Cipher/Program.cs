using System;
using System.Text;

namespace Caesar_Cipher
{
    class Program
    {
        static void Main(string[] args)
        {

            string input = Console.ReadLine();
            // input[i] + 3

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                int positionOfChar = input[i] + 3;
                output.Append((char)positionOfChar);

            }

            Console.WriteLine(output);

        }
    }
}
