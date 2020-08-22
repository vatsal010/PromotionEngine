using System;

namespace PromotionEngine.Model.Model.Interfaces
{
    /// <summary>
    /// The product that can be added in Cart
    /// </summary>
    public interface IProduct
    {
        Guid Id { get; }

        string Name { get; set; }

        double Price { get; set; }

        string Description { get; set; }
    }
}
