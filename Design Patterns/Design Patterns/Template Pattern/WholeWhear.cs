using System;
using System.Collections.Generic;
using System.Text;

namespace Template_Pattern
{
    public class WholeWheat : BreadBase
    {
        public override void Bake()
        {
            Console.WriteLine($"Baking whole wheat bread for 20 minutes");
        }

        public override void MixIngridients()
        {
            Console.WriteLine($"gathering ingredients for whole wheat bread");
        }
    }
}
