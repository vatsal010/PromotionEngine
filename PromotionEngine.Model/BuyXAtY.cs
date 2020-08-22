using PromotionEngine.Model.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Model
{
    public class BuyXAtY : IDiscount
    {
        private readonly int _buyCount = 0;
        private readonly double _discountPrice = 0;
        private IProduct _product;

        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public BuyXAtY(int buyCount, double discountPrice, IProduct product)
        {
            Id = Guid.NewGuid();
            _discountPrice = discountPrice;
            _buyCount = buyCount;
            _product = product;
        }

        public double Apply(List<CartItem> cart)
        {
            int itemCount = cart.Where(x => x.Id == _product.Id).Count();

            var itemPacks = itemCount / this._buyCount;
            var itemPacksRemainder = itemCount % this._buyCount;
            var total = 0.0;

            if (itemPacks > 0)
            {
                total = (itemPacks * this._discountPrice) + (itemPacksRemainder * _product.Price);
            }
            else
            {
                total = itemCount * _product.Price;
            }
            return total;
        }
    }
}
