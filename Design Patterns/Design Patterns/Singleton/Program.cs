using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            var db1 = SingleDataContainer.Instance;
            var db2 = SingleDataContainer.Instance;
            var db3 = SingleDataContainer.Instance;
            var db4 = SingleDataContainer.Instance;

        }
    }
}
