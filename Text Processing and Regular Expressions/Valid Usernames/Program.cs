using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Valid_Usernames
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] usernames = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);

            List<string> result = new List<string>();
            
            for (int i = 0; i < usernames.Length; i++)
            {
                bool length = false;
                bool contains = true;

                char[] word = usernames[i].ToCharArray();
                
                if (usernames[i].Length <= 16 && usernames[i].Length >= 3)
                {
                    length = true;
                }
                for (int j = 0; j < word.Length; j++)
                {
                    if (!char.IsLetterOrDigit(word[j]) && word[j] != '-' && word[j] != '_')
                    {
                        contains = false;
                        break;
                    }
                }
                if (length && contains)
                {
                    result.Add(usernames[i]);
                }


            }

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}
