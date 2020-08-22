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

        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<IProduct> Products { get; set; }

        public BuyXAtY(int buyCount, double discountPrice, List<IProduct> products)
        {
            Id = Guid.NewGuid();
            _discountPrice = discountPrice;
            _buyCount = buyCount;
            Products = products;
        }

        public double Apply(List<CartItem> cart)
        {
            int itemCount = cart.Where(x => x.Product.Id == Products.FirstOrDefault().Id).Select(x => x.Count).FirstOrDefault();

            var itemPacks = itemCount / this._buyCount;
            var itemPacksRemainder = itemCount % this._buyCount;
            var total = 0.0;

            if (itemPacks > 0)
            {
                total = (itemPacks * this._discountPrice) + (itemPacksRemainder * Products.FirstOrDefault().Price);
            }
            else
            {
                total = itemCount * Products.FirstOrDefault().Price;
            }
            return total;
        }
    }
}
