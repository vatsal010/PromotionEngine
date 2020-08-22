namespace PromotionEngine.Model.Model.Interfaces
{
    public interface ICart
    {
        double Add(IProduct product);

        bool Remove(IProduct product, int count);

        double Checkout();
    }
}