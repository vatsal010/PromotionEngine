using PromotionEngine.Model.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<IProduct> Products { get; set; }

        public BuyXAndYAtZ(double discountPrice, List<IProduct> products)
        {
            Id = Guid.NewGuid();
            _discountPrice = discountPrice;
            Products = products;
        }

        public double Apply(List<CartItem> cart)
        {
            var total = 0.0;
            var ProductX = Products[0];
            var ProductY = Products[1];

            var itemXCount = cart.Where(x => x.Id == ProductX.Id).Count();
            var itemYCount = cart.Where(x => x.Id == ProductY.Id).Count();

            var itemXPack = itemXCount / 2;
            var itemXRemainder = itemXCount % 2;
            var itemYPack = itemYCount / 2;
            var itemYRemainder = itemYCount % 2;

            if (itemXPack > itemYPack)
            {
                total = (itemYPack * this._discountPrice) + ((itemXPack - itemYPack) * ProductX.Price);
            }
            else if (itemXPack < itemYPack)
            {
                total = (itemXPack * this._discountPrice) + ((itemYPack - itemXPack) * ProductY.Price);
            }
            else
            {
                total = (itemXPack * this._discountPrice);
            }

            if (itemXRemainder > 0)
            {
                total = total + ProductX.Price;
            }
            if (itemYRemainder > 0)
            {
                total = total + ProductY.Price;
            }

            return total;
        }
    }
}
