namespace MakeASalad
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class MakeASalad
    {
        public static void Main(string[] args)
        {
            List<string> inpuProducts = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            List<double> inputCalories = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToList();

            Queue<string> products = new Queue<string>(inpuProducts);

            Stack<double> calories = new Stack<double>(inputCalories);

            Queue<double> finishedSalads = new Queue<double>();
            ;

            while (products.Count > 0 && calories.Count > 0)
            {
                string eachProduct = products.Dequeue(); 
                double currentSaladCalorie = calories.Pop();

                double currentProductCal = CalorieForProduct(eachProduct);

                if (currentSaladCalorie > currentProductCal)
                {
                    currentSaladCalorie -= currentProductCal;
                    calories.Push(currentSaladCalorie);
                }// 215  123 
                else if (currentProductCal >= currentSaladCalorie)
                {
                    if (inpuProducts.Any())
                    {

                        finishedSalads.Enqueue(inputCalories[inputCalories.Count - 1]);

                        inputCalories.RemoveAt(inputCalories.Count - 1);
                    }

                }

                if (products.Count == 0 && calories.Any())
                {
                    finishedSalads.Enqueue(inputCalories[inputCalories.Count - 1]);
                    if (calories.Any())
                    {
                        calories.Pop();
                    }
                }
                else if (calories.Count == 0 && products.Any())
                {

                }
            }

            PrintInfo(products, calories, finishedSalads);
        }

        public static void PrintInfo(Queue<string> products, Stack<double> calories, Queue<double> finishedSalads)
        {
            if (finishedSalads.Any())
            {

                Console.WriteLine(string.Join(" ", finishedSalads));
            }

            if (products.Any())
            {
                Console.WriteLine(string.Join(" ", products));
            }
            else if (calories.Any())
            {
                Console.WriteLine(string.Join(" ", calories));
            }
        }

        private static double CalorieForProduct(string eachProduct)
        {
            int currentvalue = 0;
            if (eachProduct == "tomato")
            {
                currentvalue = 80;
            }
            else if (eachProduct == "carrot")
            {
                currentvalue = 136;
            }
            else if (eachProduct == "lettuce")
            {
                currentvalue = 109;
            }
            else if (eachProduct == "potato")
            {
                currentvalue = 215;
            }

            return currentvalue;
        }
    }
}
