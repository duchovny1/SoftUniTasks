using System;
using System.Linq;

namespace Extract_File
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Console.ReadLine();

            int startingIndex = path.LastIndexOf('\\') + 1;

            string file = path.Substring(startingIndex);

            string[] allFileName = file.Split('.');

            Console.WriteLine($"File name: {allFileName[0]}");
            Console.WriteLine($"File extension: {allFileName[1]}");




        }
    }
}
