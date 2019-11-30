using System;
using System.Text;

namespace Text_Processing_and_Regular_Expressions
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            string digits = "";
            string letters = "";
            string others = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i]))
                {

                    digits += text[i];
                }
                else if (char.IsLetter(text[i]))
                {
                    letters += text[i];
                }

               else  
                {
                    others += text[i];
                }

            }

            Console.WriteLine(digits);
            Console.WriteLine(letters);
            Console.WriteLine(others);

        }
    }
}
