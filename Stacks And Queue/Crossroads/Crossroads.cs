namespace Crossroads
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Crossroads
    {
        static void Main(string[] args)
        {
            int greenLightDuration = int.Parse(Console.ReadLine());
            int freeWindowDuration = int.Parse(Console.ReadLine());
            var carsInTraffic = new Queue<string>();
            bool isCrashHappenned = false;
            int positionOfCrash = 0;
            string carWhoCrashed = string.Empty;

            string command;
            int countOfcars = 0;
            while (true)
            {
                command = Console.ReadLine();

                if (command == "END")
                {
                    break;
                }
                else if (command != "green")
                {
                    carsInTraffic.Enqueue(command);
                    continue;
                }
                else
                {
                    int currentGreenLight = greenLightDuration;

                    while (currentGreenLight > 0 && carsInTraffic.Count != 0)
                    {
                        string currentCar = carsInTraffic.Dequeue(); // check this out after
                        int currentDuration = currentCar.Length;

                        if (currentGreenLight - currentDuration >= 0)
                        {
                            currentGreenLight -= currentDuration;
                            countOfcars++;
                        }
                        else
                        {

                            int timeToPass = currentGreenLight + freeWindowDuration;

                            if (timeToPass >= currentCar.Length) // time is enough to pass
                            {
                                currentGreenLight -= currentCar.Length;
                                countOfcars++;
                            }
                            else if (timeToPass < currentCar.Length) // hitted
                            {
                                carWhoCrashed = currentCar;
                                positionOfCrash = timeToPass;
                                isCrashHappenned = true;
                                break;
                            }
                        }

                    }
                    if (isCrashHappenned)
                    {
                        break;
                    }
                }
            }

            if (isCrashHappenned)
            {
                Console.WriteLine($"A crash happened!");
                Console.WriteLine($"{carWhoCrashed} was hit at {carWhoCrashed[positionOfCrash]}.");
            }
            else
            {
                Console.WriteLine("Everyone is safe.");
                Console.WriteLine($"{countOfcars} total cars passed the crossroads.");
            }


        }
    }
}
