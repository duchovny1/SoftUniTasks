using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Singleton
{
    public class SingleDataContainer : ISingletonContainer
    {
        private Dictionary<string, int> capitals = new Dictionary<string, int>();

        private static SingleDataContainer instance = new SingleDataContainer();

        private SingleDataContainer()
        {
            Console.WriteLine($"Initilizing singleton object");

            //var elements = File.ReadAllLines("capitals.txt");

            //for (int i = 0; i < elements.Length; i++)
            //{
            //    capitals.Add(elements[i], int.Parse(elements[i] + 1));
            //}
        }

        public static SingleDataContainer Instance => instance;



        public int GetPopulation(string name)
        {
            return capitals[name];
        }
    }
}
