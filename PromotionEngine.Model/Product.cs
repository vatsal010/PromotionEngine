using System;

namespace PromotionEngine.Model.Model.Interfaces
{
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
