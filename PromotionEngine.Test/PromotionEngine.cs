using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine.Model;
using PromotionEngine.Model.InMemoryDiscountDB;
using PromotionEngine.Model.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Test
{
    [TestClass]
    public class PromotionEngine
    {
        private List<IProduct> products = null;
        private IDiscountDb discountDb = null;

        [TestCategory("Smoke"), TestInitialize]
        public void Initialize()
        {
            products = CreateProducts();
            discountDb = new DiscountDb();
            var productDiscountListCD = products.Where(x => x.Name == "Lays" || x.Name == "Dairy Milk").ToList();
            var productDiscountListA = products.Where(x => x.Name == "Colgate").ToList();
            var productDiscountListB = products.Where(x => x.Name == "Detol").ToList();
            AssignBuyXAndYAtZDiscount(productDiscountListCD, 30, discountDb);
            AssignBuyXAtYDiscount(productDiscountListA, 3, 130, discountDb);
            AssignBuyXAtYDiscount(productDiscountListB, 2, 45, discountDb);
        }

        [TestCategory("Smoke"), TestMethod]
        public void AddItems_NoDiscount()
        {
            // Arragge
            var cart = new Cart(discountDb);
            cart.Add(products[0]);
            cart.Add(products[1]);
            cart.Add(products[2]);

            // Act
            var result = cart.Checkout();

            // Assert
            Assert.IsTrue(100 == result);
        }

        [TestCategory("Smoke"), TestMethod]
        public void RemoveItems_RemoveValidItem()
        {
            // Arragge
            var cart = new Cart(discountDb);
            cart.Add(products[0]);

            // Act
            var result = cart.Remove(products[0], 1);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCategory("Smoke"), TestMethod]
        public void RemoveItems_RemoveInValidItem()
        {
            // Arragge
            var cart = new Cart(discountDb);
            cart.Add(products[0]);

            // Act
            var result = cart.Remove(products[1], 1);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCategory("Smoke"), TestMethod]
        public void AddItems_DiscountOnProductAB()
        {
            // Arragge
            var cart = new Cart(discountDb);
            cart.Add(products[0]);
            cart.Add(products[0]);
            cart.Add(products[0]);
            cart.Add(products[0]);
            cart.Add(products[0]);
            cart.Add(products[1]);
            cart.Add(products[1]);
            cart.Add(products[1]);
            cart.Add(products[1]);
            cart.Add(products[1]);
            cart.Add(products[2]);

            // Act
            var result = cart.Checkout();

            // Assert
            Assert.IsTrue(370 == result);
        }

        [TestCategory("Smoke"), TestMethod]
        public void AddItems_DiscountOnProductABC()
        {
            // Arragge
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

            // Act
            var result = cart.Checkout();

            // Assert
            Assert.IsTrue(280 == result);
        }

        [TestCategory("Smoke"), TestMethod]
        public void AddItems_DiscountOnProductCD_ExtraProductC()
        {
            // Arragge
            var cart = new Cart(discountDb);
            cart.Add(products[2]);
            cart.Add(products[3]); //30
            cart.Add(products[2]);
            cart.Add(products[3]); //30
            cart.Add(products[2]);
            cart.Add(products[3]); //30

            cart.Add(products[2]); //20
            cart.Add(products[2]); //20

            // Act
            var result = cart.Checkout();

            // Assert
            Assert.IsTrue(130 == result);
        }

        [TestCategory("Smoke"), TestMethod]
        public void AddItems_DiscountOnProductCD_ExtraProductD()
        {
            // Arragge
            var cart = new Cart(discountDb);
            cart.Add(products[2]);
            cart.Add(products[3]); //30
            cart.Add(products[2]);
            cart.Add(products[3]); //30
            cart.Add(products[2]);
            cart.Add(products[3]); //30

            cart.Add(products[3]); //15
            cart.Add(products[3]); //15

            // Act
            var result = cart.Checkout();

            // Assert
            Assert.IsTrue(120 == result);
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
