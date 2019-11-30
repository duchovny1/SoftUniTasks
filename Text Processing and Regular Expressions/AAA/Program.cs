using System;
using System.Collections.Generic;
using System.Linq;

namespace Concert
{
    class Program
    {
        static void Main(string[] args)
        {
            var gamesWithPrices = new Dictionary<string, double>();

            var gamesWithDlc = new Dictionary<string, string>();

            string[] commands = Console.ReadLine().Split(", ");

            for (int i = 0; i < commands.Length; i++)
            {
                if (commands[i].Contains('-'))
                {
                    string[] splitArrForEachGame = commands[i].Split('-');
                    string game = splitArrForEachGame[0];
                    double price = double.Parse(splitArrForEachGame[1]);

                    if (!gamesWithPrices.ContainsKey(game))
                    {
                        gamesWithPrices[game] = price;
                    }


                }
                else if (commands[i].Contains(':'))
                {
                    string[] splitArrForEachGame = commands[i].Split(':');
                    string game = splitArrForEachGame[0];
                    string DLC = splitArrForEachGame[1];

                    if (gamesWithPrices.ContainsKey(game))
                    {
                        if (!gamesWithDlc.ContainsKey(game))
                        {
                            gamesWithDlc[game] = DLC;
                        }
                    }
                }
            }

            foreach (var kvp in gamesWithPrices.OrderBy(x => x.Value))
            {
                foreach (var item in gamesWithDlc)
                {
                    if (kvp.Key == item.Key)
                    {
                        double totalPrice = kvp.Value + 0.20 * kvp.Value;
                        totalPrice = totalPrice - 0.50 * totalPrice;
                        Console.WriteLine($"{kvp.Key} - {item.Value} - {totalPrice:f2}");
                        gamesWithPrices.Remove(kvp.Key);
                    }
                }
            }



            foreach (var kvp in gamesWithPrices.OrderByDescending(x => x.Value))
            {
                double totalPrice = kvp.Value - 0.20 * kvp.Value;
                Console.WriteLine($"{kvp.Key} - {totalPrice:f2}");
            }


        }
    }
}