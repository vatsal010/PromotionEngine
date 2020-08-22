using PromotionEngine.Model;
using PromotionEngine.Model.InMemoryDiscountDB;
using PromotionEngine.Model.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var products = CreateProducts();
            var productDiscountListCD = products.Where(x => x.Name == "Lays" || x.Name == "Dairy Milk").ToList();
            var productDiscountListA = products.Where(x => x.Name == "Colgate").ToList();
            var productDiscountListB = products.Where(x => x.Name == "Detol").ToList();
            var discountDb = new DiscountDb();
            AssignBuyXAndYAtZDiscount(productDiscountListCD, 30, discountDb);
            AssignBuyXAtYDiscount(productDiscountListA, 3, 130, discountDb);
            AssignBuyXAtYDiscount(productDiscountListB, 2, 45, discountDb);

            var cart = new Cart(discountDb);
            cart.Add(products[0]);
            cart.Add(products[0]);
            cart.Add(products[0]);
            cart.Add(products[1]);
            cart.Add(products[1]);
            cart.Add(products[1]);
            cart.Add(products[1]);
            cart.Add(products[1]);
            cart.Add(products[2]);
            cart.Add(products[3]);

            var totalPrice = cart.Checkout();
            Console.WriteLine(totalPrice);
            Console.ReadLine();
        }

        private static List<IProduct> CreateProducts()
        {
            List<IProduct> products = new List<IProduct>();

            products.Add(new Product() { Price = 50, Description = "Tooth Paste", Name = "Colgate" });
            products.Add(new Product() { Price = 30, Description = "Soap", Name = "Detol" });
            products.Add(new Product() { Price = 20, Description = "Cheaps", Name = "Lays" });
            products.Add(new Product() { Price = 15, Description = "Chocolate", Name = "Dairy Milk" });

            return products;
        }

        private static void AssignBuyXAndYAtZDiscount(List<IProduct> products, double price, IDiscountDb discountDb)
        {
            IDiscount discount1 = new BuyXAndYAtZ(price, products);
            discount1.IsActive = true;

            discountDb.Add(discount1);
        }

        private static void AssignBuyXAtYDiscount(List<IProduct> products, int buyCount, double price, IDiscountDb discountDb)
        {
            IDiscount discount1 = new BuyXAtY(buyCount, price, products);
            discount1.IsActive = true;

            discountDb.Add(discount1);
        }
    }
}
