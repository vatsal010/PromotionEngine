using PromotionEngine.Model.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Model
{
    public class BuyXAndYAtZ : IDiscount
    {
        private readonly double _discountPrice = 0;
        private IProduct _productX;
        private IProduct _productY;

        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public BuyXAndYAtZ(double discountPrice, IProduct productX, IProduct productY)
        {
            Id = Guid.NewGuid();
            _discountPrice = discountPrice;
            _productX = productX;
            _productY = productY;
        }

        public double Apply(List<CartItem> cart)
        {
            var total = 0.0;
            var itemXCount = cart.Where(x => x.Id == _productX.Id).Count();
            var itemYCount = cart.Where(x => x.Id == _productY.Id).Count();

            var itemXPack = itemXCount / 2;
            var itemXRemainder = itemXCount % 2;
            var itemYPack = itemYCount / 2;
            var itemYRemainder = itemYCount % 2;

            if (itemXPack > itemYPack)
            {
                total = (itemYPack * this._discountPrice) + ((itemXPack - itemYPack) * _productX.Price);
            }
            else if (itemXPack < itemYPack)
            {
                total = (itemXPack * this._discountPrice) + ((itemYPack - itemXPack) * _productY.Price);
            }
            else
            {
                total = (itemXPack * this._discountPrice);
            }

            if (itemXRemainder > 0)
            {
                total = total + _productX.Price;
            }
            if (itemYRemainder > 0)
            {
                total = total + _productY.Price;
            }

            return total;
        }
    }
}
