using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Star_Enigma
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfMessages = int.Parse(Console.ReadLine());
            List<string> messages = new List<string>();

            var attackedPlanets = new List<string>();
            var destroyedPlanets = new List<string>();




            for (int i = 0; i < numberOfMessages; i++)
            {

                var singleMessage = new StringBuilder(Console.ReadLine());
                int count = 0;
                for (int j = 0; j < singleMessage.Length; j++)
                {
                    if (singleMessage[j] == 's' || singleMessage[j] == 'S' ||
                        singleMessage[j] == 't' || singleMessage[j] == 'T' ||
                        singleMessage[j] == 'a' || singleMessage[j] == 'A' ||
                        singleMessage[j] == 'r' || singleMessage[j] == 'R')

                    {
                        count++;
                    }
                }
                for (int j = 0; j < singleMessage.Length; j++)
                {

                    int num = singleMessage[j] - count;
                    singleMessage[j] = (char)num;
                }

                string planetName = "";
                string population = "";
                string typeOfAttack = "";
                string countSoldiers = "";

                MatchCollection regex = Regex.Matches(singleMessage.ToString(), @"@(?<planet>[A-Za-z]+)([^@:!\\>]*):(?<population>[0-9]+)([^@:!\\>]*)!(?<typeAttack>[A-Z]{1})!([^@:!\\>]*)->(?<soldierCount>[0-9]+)");

                foreach (Match match in regex)
                {
                    planetName = match.Groups["planet"].Value;
                    population = match.Groups["population"].Value;
                    typeOfAttack = match.Groups["typeAttack"].Value;

                }
               


                if (planetName == "" || typeOfAttack == "" || countSoldiers == "")
                {
                    continue;
                }

                else if (typeOfAttack == "A")
                {
                    attackedPlanets.Add(planetName);
                }
                else if (typeOfAttack == "D")
                {
                    destroyedPlanets.Add(planetName);
                }

            }

            var attackedListSorted = attackedPlanets.OrderBy(x => x).ToList();
            var destroyedListSorted = destroyedPlanets.OrderBy(x => x).ToList();


            Console.WriteLine($"Attacked planets: {attackedPlanets.Count}");
            foreach (var item in attackedListSorted)
            {
                Console.WriteLine($"-> {item}");
            }
            Console.WriteLine($"Destroyed planets: {destroyedPlanets.Count}");

            
            foreach (var item in destroyedListSorted)
            {
                Console.WriteLine($"-> {item}");
            }



        }
    }
}// a b c d
