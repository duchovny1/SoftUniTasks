using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Deciphering_Demo_Exam_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            string lineToDecode = Console.ReadLine(); /// c c c
            string[] substringsToReplace = Console.ReadLine().Split();
            StringBuilder decodedLine = new StringBuilder();
            
            bool isValid = Regex.IsMatch(lineToDecode, @"[^d-z{}|#]");
            if (isValid)
            {
                Console.WriteLine("This is not the book you are looking for.");
                return;
            }
            
            for (int i = 0; i < lineToDecode.Length; i++)
            {
                int num = (int)lineToDecode[i] - 3;
                char decodedChar = (char)num;
                decodedLine.Append(decodedChar);
            }

            string replacedString = decodedLine.Replace(substringsToReplace[0], substringsToReplace[1]).ToString();
            Console.WriteLine(replacedString);
        }
    }
}
