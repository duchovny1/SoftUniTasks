namespace Truck_Tour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BalancedParentheses

    {

        public static void Main()
        {
            char[] input = Console.ReadLine().ToCharArray();


            Stack<char> firstStack = new Stack<char>();
            Stack<char> secondStack = new Stack<char>();
            char[] operators = { '}', ']', ')' };
            bool IsBalanced = false;
            for (int i = 0; i < input.Length; i++)
            {
                if (input.Length % 2 == 0)
                {
                    break;
                }
                if (i <= input.Length / 2 - 1)
                {
                    firstStack.Push(input[i]);
                }

                else if (firstStack.Peek() == '{' && input[i] == '}' ||
                     firstStack.Peek() == '(' && input[i] == ')' ||
                     firstStack.Peek() == '[' && input[i] == ']')
                {
                    if (i > input.Length / 2 - 1)
                    {
                        firstStack.Pop();
                        IsBalanced = true;
                    }
                }
                else
                {
                    IsBalanced = false;

                }

            }
            if (IsBalanced)
            {
                Console.WriteLine("YES");
            }
            else if (IsBalanced == false)
            {
                Console.WriteLine("NO");
            }

        }

    }
}
