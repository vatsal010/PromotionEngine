using PromotionEngine.Model.Model.Interfaces;
using System;

namespace PromotionEngine.Model
{
    /// <summary>
    /// The product that can be added in Cart
    /// </summary>
    public class Product : IProduct
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }
    }
}
