using PromotionEngine.Model.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Model
{
    /// <summary>
    /// Discount - Buy Product A * X at the price of Y
    /// E.g. Product A = 50INR, Buy Product A * 3 at 130
    /// </summary>
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

        /// <summary>
        /// Apply's discount on products if the cart has Product X in multiple of 3
        /// </summary>
        /// <param name="cart">The Cart</param>
        /// <returns>Total discounted price</returns>
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
