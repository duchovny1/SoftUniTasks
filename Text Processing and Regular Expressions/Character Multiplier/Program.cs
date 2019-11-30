using System;

namespace Character_Multiplier
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words = Console.ReadLine().Split();

            string firstWord = words[0];
            string secondWord = words[1];

            string longerWord = "";
            string shorterWord = "";

            int totalSum = 0;

            if (firstWord.Length >= secondWord.Length)
            {
                longerWord = firstWord;
                shorterWord = secondWord;
            }
            else
            {
                longerWord = secondWord;
                shorterWord = firstWord;
            }


            for (int i = 0; i < shorterWord.Length; i++)
            {
                totalSum += longerWord[i] * shorterWord[i];
            }

            for (int i = shorterWord.Length; i < longerWord.Length; i++)
            {
                totalSum += longerWord[i];
            }

            Console.WriteLine(totalSum);


        }


    }
}
