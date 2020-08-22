using PromotionEngine.Model.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Model.InMemoryDiscountDB
{
    /// <summary>
    /// In memory version of the Discount DB
    /// </summary>
    public class DiscountDb : IDiscountDb
    {
        private List<IDiscount> _discount = null;

        public DiscountDb()
        {
            _discount = new List<IDiscount>();
        }

        /// <summary>
        /// Add's discount 
        /// </summary>
        /// <param name="discount"></param>
        public void Add(IDiscount discount)
        {
            _discount.Add(discount);
        }

        /// <summary>
        /// Get's the discount for product
        /// </summary>
        /// <param name="product">The product</param>
        /// <returns>Discount of the product</returns>
        public List<IDiscount> GetProductDiscounts(IProduct product)
        {
            return _discount.Where(x => x.Products.Where(y => y.Id == product.Id).Count() > 0).ToList();
        }

        /// <summary>
        /// Remove's the discount
        /// </summary>
        /// <param name="discount"></param>
        public void Remove(IDiscount discount)
        {
            _discount.Remove(discount);
        }
    }
}
