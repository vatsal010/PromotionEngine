using PromotionEngine.Model.Model.Interfaces;
using System;

namespace PromotionEngine.Model
{
    public class BuyXAndYAtZ : IDiscount
    {
        private readonly double _discountPrice = 0;

        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public BuyXAndYAtZ(double discountPrice)
        {
            Id = Guid.NewGuid();
            _discountPrice = discountPrice;
        }

        public double Apply(int itemACount, double productAPrice, int itemBCount, double productBPrice)
        {
            var total = 0.0;
            var itemAPack = itemACount / 2;
            var itemARemainder = itemACount % 2;
            var itemBPack = itemBCount / 2;
            var itemBRemainder = itemBCount % 2;

            if (itemAPack > itemBPack)
            {
                total = (itemBPack * this._discountPrice) + ((itemAPack - itemBPack) * productAPrice);
            }
            else
            {
                total = (itemAPack * this._discountPrice) + ((itemBPack - itemAPack) * productAPrice);
            }

            if (itemARemainder > 0)
            {
                total = total + productAPrice;
            }
            if (itemBRemainder > 0)
            {
                total = total + productBPrice;
            }

            return total;
        }
    }
}
