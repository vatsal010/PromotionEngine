using System;
using System.Collections.Generic;

namespace PromotionEngine.Model.Model.Interfaces
{
    /// <summary>
    /// Interface for Discounts that can be applied
    /// </summary>
    public interface IDiscount
    {
        Guid Id { get; set; }
        bool IsActive { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        List<IProduct> Products { get; set; }

        double Apply(List<CartItem> cart);
    }
}
