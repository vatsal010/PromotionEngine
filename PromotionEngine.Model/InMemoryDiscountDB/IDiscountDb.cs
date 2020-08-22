using PromotionEngine.Model.Model.Interfaces;
using System.Collections.Generic;

namespace PromotionEngine.Model.InMemoryDiscountDB
{
    public interface IDiscountDb
    {
        void Remove(IDiscount discount);
        void Add(IDiscount discount);
        List<IDiscount> GetProductDiscounts(IProduct product);
    }
}
