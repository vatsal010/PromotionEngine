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

        private void UpdateDiscountStatus(List<CartItem> cart)
        {
            var itemX = cart.Where(x => x.Product.Id == Products[0].Id).FirstOrDefault();
            var itemY = cart.Where(x => x.Product.Id == Products[1].Id).FirstOrDefault();

            if (itemX != null && itemY != null)
            {
                itemX.IsPriceCalculated = true;
            }
        }

        public double Apply(List<CartItem> cart)
        {
            var total = 0.0;
            var ProductX = Products[0];
            var ProductY = Products[1];

            var itemXCount = cart.Where(x => x.Product.Id == ProductX.Id).Select(x => x.Count).FirstOrDefault();
            var itemYCount = cart.Where(x => x.Product.Id == ProductY.Id).Select(x => x.Count).FirstOrDefault();

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
            this.UpdateDiscountStatus(cart);
            return total;
        }
    }
}
