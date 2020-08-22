using PromotionEngine.Model.Model.Interfaces;
using System;

namespace PromotionEngine.Model
{
    public class BuyXAtY : IDiscount
    {
        private readonly int _buyCount = 0;
        private readonly double _discountPrice = 0;

        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public BuyXAtY(int buyCount, double discountPrice)
        {
            Id = Guid.NewGuid();
            _discountPrice = discountPrice;
            _buyCount = buyCount;
        }

        public double Apply(int itemCount, double productPrice)
        {
            var itemPacks = itemCount / this._buyCount;
            var itemPacksRemainder = itemCount % this._buyCount;
            var total = 0.0;

            if (itemPacks > 0)
            {
                total = (itemPacks * this._discountPrice) + (itemPacksRemainder * productPrice);
            }
            else
            {
                total = itemCount * productPrice;
            }
            return total;
        }
    }
}
