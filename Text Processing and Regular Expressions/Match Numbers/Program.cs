using System.Text.RegularExpressions;
using System;

public class Example
{
    public static void Main()
    {
        string pattern = @"(^|(?<=\s))[-]?\d*[.]?[\d]+($|(?=\s))";
        string input = Console.ReadLine();
        MatchCollection matches = Regex.Matches(input, pattern);

        foreach (Match item in matches)
        {
            Console.Write(item + " ");
        }
    }
}