namespace Simple_Text_Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            Stack<string> stateOfWords = new Stack<string>();
            StringBuilder text = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string command = input[0];

                if (command == "1")
                {
                    stateOfWords.Push(text.ToString());
                    string textToAppend = input[1];
                    text.Append(textToAppend);
                }
                else if (command == "2")
                {
                    stateOfWords.Push(text.ToString());
                    int index = int.Parse(input[1]);

                    text.Remove(text.Length - index, index);
                }
                else if (command == "3")
                {
                    int position = int.Parse(input[1]);
                    Console.WriteLine(text[position - 1]);
                }
                else if (command == "4")
                {
                    text.Clear();
                    text.Append(stateOfWords.Pop());
                }
            }

            
        }
    }
}
