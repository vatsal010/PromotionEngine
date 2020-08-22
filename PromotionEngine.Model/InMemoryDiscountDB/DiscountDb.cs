using PromotionEngine.Model.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Model.InMemoryDiscountDB
{
    public class DiscountDb : IDiscountDb
    {
        private List<IDiscount> _discount = null;

        public DiscountDb()
        {
            _discount = new List<IDiscount>();
        }

        public void Add(IDiscount discount)
        {
            _discount.Add(discount);
        }

        public List<IDiscount> GetProductDiscounts(IProduct product)
        {
            return _discount.Where(x => x.Products.Where(y => y.Id == product.Id).Count() > 0).ToList();
        }

        public void Remove(IDiscount discount)
        {
            throw new NotImplementedException();
        }
    }
}
