using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Text.RegularExpressions;

namespace NewSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var number = Console.ReadLine().TrimStart('0').ToList();
            var number2 = int.Parse(Console.ReadLine());

            var result = new List<int>();

            number.Reverse();
            var reminder = 0;

            if (number2 == 0)
            {
                Console.WriteLine(0);
                return;
            }

            foreach (var item in number)
            {
                int num = int.Parse(item.ToString());
                int numResult = num * number2 + reminder;
                reminder = numResult / 10;
                numResult = numResult % 10;
                result.Add(numResult);
            }
            if (reminder > 0)
            {
                result.Add(reminder);
            }
            result.Reverse();
            Console.WriteLine(string.Join("", result));
        }
    }
}