using System;
using System.Collections.Generic;
using System.Text;

namespace Composite_Pattern
{
    public class CompositeGift : GiftBase, IGiftOperations
    {
        List<GiftBase> gifts;
        public CompositeGift(string name, int price)
            : base(name, price)
        {
            gifts = new List<GiftBase>();
        }

        public void Add(GiftBase gift)
        {
            gifts.Add(gift);
        }

        public override int CalculateTotalPrice()
        {
            Console.WriteLine($"{name} contains the following products with prices:");

            foreach (var gift in gifts)
            {
                price += gift.CalculateTotalPrice();
            }

            return price;
        }

        public void Remove(GiftBase gift)
        {
            gifts.Remove(gift);
        }
    }
}
