using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Song_Encryption
{
    class Program
    {
        static void Main(string[] args)
        {

            string input = "";


            while ((input = Console.ReadLine()) != "end")
            {
                StringBuilder sb = new StringBuilder();
                string[] artistAndSongName = input.Split(':');
                string artist = artistAndSongName[0];
                string songName = artistAndSongName[1];

                bool isValid = Regex.IsMatch(input, @"^([A-Z]{1}[a-z'\s]+([a-z'\s]+)*):([A-Z\s]+)$");

                if (isValid == false)
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                int countOfIncrement = artist.Length;

                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == ' ' || input[i] == (char)39 || input[i] == ':')
                    {
                        sb.Append(input[i]);
                        continue;
                    }

                    int num = input[i];
                    char currentChar = (char)(num + countOfIncrement);                        

                    char newChar = currentChar;

                    if (num <= 90)
                    {
                        if (currentChar > 90)
                        { // 6 - 96 - 90
                            
                            newChar = (char)(currentChar - 26);
                        }

                    }
                    else if (num >= 97 && num <= 122)
                    {
                        if (currentChar > 122)
                        { // 6 - 96 - 90
                            
                            newChar = (char)(currentChar - 26);
                        }

                    }
                    sb.Append(newChar);
                }

                sb.Replace(':', '@');

                Console.WriteLine($"Successful encryption: {sb}");
            }


        }
    }
}
