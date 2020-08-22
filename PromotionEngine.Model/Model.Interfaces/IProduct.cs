using System;

namespace PromotionEngine.Model.Model.Interfaces
{
    public interface IProduct
    {
        Guid Id { get; }

        string Name { get; set; }

        double Price { get; set; }

        string Description { get; set; }
    }
}
