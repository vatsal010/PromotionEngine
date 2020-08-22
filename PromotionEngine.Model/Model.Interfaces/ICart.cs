namespace PromotionEngine.Model.Model.Interfaces
{
    /// <summary>
    /// Shopping cart
    /// </summary>
    public interface ICart
    {
        double Add(IProduct product);

        bool Remove(IProduct product, int count);

        double Checkout();
    }
}