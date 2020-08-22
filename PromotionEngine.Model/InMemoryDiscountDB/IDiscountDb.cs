using PromotionEngine.Model.Model.Interfaces;
using System.Collections.Generic;

namespace PromotionEngine.Model.InMemoryDiscountDB
{
    /// <summary>
    /// In memory version of the Discount DB
    /// </summary>
    public interface IDiscountDb
    {
        void Remove(IDiscount discount);
        void Add(IDiscount discount);
        List<IDiscount> GetProductDiscounts(IProduct product);
    }
}
