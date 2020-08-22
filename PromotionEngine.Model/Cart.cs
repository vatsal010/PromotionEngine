using PromotionEngine.Model.InMemoryDiscountDB;
using PromotionEngine.Model.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Model
{
    /// <summary>
    /// Shopping cart
    /// </summary>
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

        /// <summary>
        /// Add product in cart
        /// </summary>
        /// <param name="product">Product to be added in cart</param>
        /// <returns>Total cost of the Product(s)</returns>
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

            return productTotalPrice;
        }

        /// <summary>
        /// Checkout products from the cart
        /// </summary>
        /// <returns>Total cost of all the products</returns>
        public double Checkout()
        {
            _totalPrice = _cart.Where(x => x.IsPriceCalculated == false).Sum(x => x.TotalPrice);
            return _totalPrice;
        }

        /// <summary>
        /// Remove item from cart
        /// </summary>
        /// <param name="product">Product to be removed</param>
        /// <param name="count">Count of the product to be removed</param>
        /// <returns></returns>
        public bool Remove(IProduct product, int count)
        {
            var result = false;
            var productCartItem = _cart.FirstOrDefault(x => x.Product.Id == product.Id);

            if (productCartItem != null && productCartItem.Count >= count)
            {
                productCartItem.Count = productCartItem.Count - count;
                result = true;
            }
            return result;
        }
    }
}
