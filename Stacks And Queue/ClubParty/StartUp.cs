namespace ClubParty
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class StartUp
    {
        static void Main(string[] args)
        {
            int hallsCapacity = int.Parse(Console.ReadLine());

            string[] input = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Stack<string> elements = new Stack<string>(input);
            Queue<string> halls = new Queue<string>();

            int tempCount = 0;
            List<int> currentElements = new List<int>();
            while (elements.Count != 0)
            {
                string currentElement = elements.Pop();
                bool isPeople = int.TryParse(currentElement, out int peopleToEnter);

                if (!isPeople)
                {
                    halls.Enqueue(currentElement);
                }
                else
                {
                    tempCount += peopleToEnter;
                    currentElements.Add(peopleToEnter);

                    if (tempCount + peopleToEnter > hallsCapacity)
                    {
                        tempCount -= peopleToEnter;
                        Console.WriteLine($"{halls.Dequeue()} -> {string.Join(", ", currentElements)}");
                        elements.Push(currentElement);
                        currentElements.Clear();
                    }
                }
                ;

            }

        }
    }
}
