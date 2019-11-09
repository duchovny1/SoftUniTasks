using System;
using System.Collections.Generic;
using System.Linq;

namespace Truck_Tour
{
    class TruckTour
    {
        static void Main(string[] args)
        {
            var input = int.Parse(Console.ReadLine());

            Queue<int[]> petrolPumps = new Queue<int[]>();


            int index = 0;
            for (int i = 0; i < input; i++)
            {
                int[] line = Console.ReadLine()
                     .Split()
                     .Select(int.Parse)
                     .ToArray();

                petrolPumps.Enqueue(line);
            }

            while (true)
            {

                int petrol = 0;
                foreach (var petrolPump in petrolPumps)
                {

                    int amountOfPetrol = petrolPump[0];
                    int distanceToNextPump = petrolPump[1];

                    petrol += (amountOfPetrol - distanceToNextPump);

                    if (petrol < 0)
                    {
                        petrolPumps.Enqueue(petrolPumps.Dequeue());
                        index++;
                        break;
                    }

                }
                if (petrol >= 0)
                {
                    break;
                }

            }

            Console.WriteLine(index);
        }
    }
}
