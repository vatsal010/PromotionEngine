using PromotionEngine.Model.Model.Interfaces;
using System;

namespace PromotionEngine.Model
{
    /// <summary>
    /// Item in the cart
    /// </summary>
    public class CartItem
    {
        public CartItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public IProduct Product { get; set; }

        public int Count { get; set; }

        public double TotalPrice { get; set; }

        public bool IsPriceCalculated { get; set; }
    }
}
