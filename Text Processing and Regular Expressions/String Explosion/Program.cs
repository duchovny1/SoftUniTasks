using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace String_Explosion
{
    class Program
    {
        static void Main(string[] args) // abc>>1>2asdada
        {
            string input = Console.ReadLine();
            int power = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '>')
                {
                    power += int.Parse(input[i + 1].ToString());
                    continue;
                }
                if (power > 0)
                {
                   input = input.Remove(i, 1);
                    i--;
                    power--;
                }

            }

            Console.WriteLine(input);
        }
    }
}
