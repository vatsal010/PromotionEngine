using PromotionEngine.Model.InMemoryDiscountDB;
using PromotionEngine.Model.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Model
{
    public class Cart : ICart
    {
        private readonly List<CartItem> _cart = null;
        private readonly IDiscountDb _discountDb = null;
        private double _totalPrice = 0;

        public Cart(IDiscountDb discountDb)
        {
            _cart = new List<CartItem>();
            _discountDb = discountDb;
        }

        public double Add(IProduct product)
        {
            var productCartItem = _cart.FirstOrDefault(x => x.Product.Id == product.Id);

            if (productCartItem == null)
            {
                var cartItem = new CartItem()
                {
                    Product = product
                };

                _cart.Add(cartItem);
                productCartItem = cartItem;
            }

            var discounts = _discountDb.GetProductDiscounts(productCartItem.Product);
            productCartItem.Count = productCartItem.Count + 1;
            var productTotalPrice = 0.0;

            if (discounts != null && discounts.Count > 0)
            {
                foreach (var discount in discounts)
                {
                    productTotalPrice = productTotalPrice + discount.Apply(_cart);
                }
            }
            else
            {
                productTotalPrice = productCartItem.TotalPrice + product.Price;
            }

            productCartItem.TotalPrice = productTotalPrice;
            _totalPrice = _cart.Sum(x => x.TotalPrice);

            return _totalPrice;
        }

        public double Checkout()
        {
            _totalPrice = _cart.Where(x => x.IsPriceCalculated == false).Sum(x => x.TotalPrice);
            return _totalPrice;
        }

        public double Remove(IProduct product, int count)
        {
            throw new NotImplementedException();
        }
    }
}
