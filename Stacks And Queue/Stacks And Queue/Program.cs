using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Basic_Stack_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] vehicles = Console.ReadLine().Split().ToArray();

            Stack<string> served = new Stack<string>();
            Queue<string> service = new Queue<string>(vehicles);


            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                if (command == "Service")
                {
                    if (service.Count != 0)
                    {
                        Console.WriteLine($"Vehicle {service.Peek()} got served.");

                        served.Push(service.Dequeue());
                    }
                }
                else if (command.Contains("CarInfo"))
                {
                    string[] infoForCar = command.Split('-');
                    string car = infoForCar[1];

                    if (service.Contains(car))
                    {
                        Console.WriteLine($"Still waiting for service.");
                    }
                    else if (served.Contains(car))
                    {
                        Console.WriteLine("Served.");
                    }
                }
                else if (command == "History")
                {
                    Console.Write(string.Join(", ", served));
                    Console.WriteLine();
                }
            }

            if (service.Count != 0)
            {
                Console.Write("Vehicles for service: ");
                Console.Write(string.Join(", ", service));
                Console.WriteLine();
            }

            Console.Write("Served vehicles: ");

            Console.WriteLine(string.Join(", ", served));
        }
    }
}