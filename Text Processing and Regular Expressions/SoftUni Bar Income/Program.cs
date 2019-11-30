using System;
using System.Text.RegularExpressions;

namespace SoftUni_Bar_Income
{
    class Program
    {
        static void Main(string[] args)
        {

            int totalIncome = 0;
            string input = Console.ReadLine();

            while (input != "end of shift")
            {
                string name = "";
                string product = "";
                string price = "";
                string countOfProducts = "";

                string regex = @"%(?<name>[A-Z][a-z]+)%<(?<product>[A-Z][a-z]+)>|(?<count>[0-9]+(?=[|]))|(?<price>[0-9]+[.]*[0-9]+(?!$))";

                MatchCollection matches = Regex.Matches(input, regex);

                foreach (Match item in matches)
                {
                    name = item.Groups["name"].Value;
                    product = item.Groups["product"].Value;
                    price = item.Groups["price"].Value;
                    countOfProducts = item.Groups["count"].Value;
                }
                    Console.WriteLine(name);
                    Console.WriteLine(product);
                    Console.WriteLine(price);
                    Console.WriteLine(countOfProducts);

                input = Console.ReadLine();
            }
        }
    }
}
