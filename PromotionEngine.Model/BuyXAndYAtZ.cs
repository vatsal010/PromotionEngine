using PromotionEngine.Model.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Model
{
    /// <summary>
    /// Discount - Buy Product A + B at the price of Y
    /// E.g. Product A = 20INR and Product B = 15, Buy Product A + B at 30
    /// </summary>
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

        /// <summary>
        /// Apply's discount on products if the cart has both Product X and Y
        /// </summary>
        /// <param name="cart">The Cart</param>
        /// <returns>Total discounted price</returns>
        public double Apply(List<CartItem> cart)
        {
            var total = 0.0;
            var ProductX = Products[0];
            var ProductY = Products[1];

            var itemXCount = cart.Where(x => x.Product.Id == ProductX.Id).Select(x => x.Count).FirstOrDefault();
            var itemYCount = cart.Where(x => x.Product.Id == ProductY.Id).Select(x => x.Count).FirstOrDefault();
            var itemX = cart.Where(x => x.Product.Id == ProductX.Id).FirstOrDefault();
            var itemY = cart.Where(x => x.Product.Id == ProductY.Id).FirstOrDefault();

            if (itemXCount > itemYCount)
            {
                total = (itemYCount * this._discountPrice) + ((itemXCount - itemYCount) * ProductX.Price);
            }
            else if (itemXCount < itemYCount)
            {
                total = (itemXCount * this._discountPrice) + ((itemYCount - itemXCount) * ProductY.Price);
            }
            else
            {
                total = (itemXCount * this._discountPrice);
            }
            this.UpdateDiscountStatus(cart, itemXCount, itemYCount, itemX, itemY);
            return total;
        }

        /// <summary>
        /// Mark's one product as Discount calculated,
        /// so that the total price is not calculated while checkout
        /// </summary>
        /// <param name="cart"></param>
        private void UpdateDiscountStatus(List<CartItem> cart, int itemXCount, int itemYCount, CartItem itemX, CartItem itemY)
        {
            if (itemXCount > itemYCount)
            {
                if(itemY != null) itemY.IsPriceCalculated = true;
                if(itemX != null) itemX.IsPriceCalculated = false;
            }
            else if (itemXCount < itemYCount)
            {
                if(itemY != null) itemY.IsPriceCalculated = false;
                if(itemX != null) itemX.IsPriceCalculated = true;
            }
            else
            {
                if(itemX != null) itemX.IsPriceCalculated = true;
            }
        }
    }
}
