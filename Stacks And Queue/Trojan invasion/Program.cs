namespace Trojan_invasion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static int currentWarrior;
        static int currentPlate;

        static void Main(string[] args)
        {

            int waves = int.Parse(Console.ReadLine());

            Queue<int> defense = new Queue<int>();

            int[] plates = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            bool isStillWarriors = true;
            bool isStillPlates = true;

            for (int i = 0; i < plates.Length; i++)
            {
                defense.Enqueue(plates[i]);
            }

            Queue<int> attack = new Queue<int>();

            for (int i = 0; i < waves; i++)
            {
                if (i != 0 && i % 3 == 0)
                {
                    plates = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();


                    for (int l = 0; l < plates.Length; l++)
                    {
                        defense.Enqueue(plates[l]);
                    }
                    continue;
                }
                else
                {
                    List<int> currentWaves = Console.ReadLine().Split().Select(int.Parse)
                        .Reverse().ToList();
                    for (int j = 0; j < currentWaves.Count; j++)
                    {
                        attack.Enqueue(currentWaves[j]);
                    }
                }


                if(isStillWarriors)
                {
                    currentWarrior = attack.Peek();
                }
               
                if (!isStillPlates)
                {

                     currentPlate = defense.Peek();
                }

                while (true)
                {

                    if (currentWarrior > currentPlate)
                    {
                        currentWarrior -= currentPlate;

                        defense.Dequeue();
                        if (defense.Count != 0)
                        {
                            currentPlate = defense.Dequeue();

                        }
                        else if (defense.Count == 0 && i + 1 % 3 != 0)
                        {
                            Console.WriteLine("The Trojans successfully destroyed the Spartan defense.");
                            Console.WriteLine($"Warriors left: {string.Join(", ", attack)}");
                            return;
                        }
                    }
                    else if (currentWarrior < currentPlate)
                    {
                        currentPlate -= currentWarrior;

                        attack.Dequeue();
                        if (attack.Count != 0)
                        {
                            currentWarrior = attack.Dequeue();
                        }
                        else if (attack.Count == 0 && i == waves - 1)
                        {
                            Console.WriteLine($"The Spartans successfully repulsed the Trojan attack.");
                            Console.WriteLine($"Plates left: {string.Join(", ", currentPlate)}");
                            return;
                        }
                    }
                    else if (currentWarrior == currentPlate)
                    {
                        if (defense.Count != 0)
                        {
                            defense.Dequeue();
                        }
                        if (attack.Count != 0)
                        {
                            attack.Dequeue();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

        }


    }
}
