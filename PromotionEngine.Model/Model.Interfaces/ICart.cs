namespace PromotionEngine.Model.Model.Interfaces
{
    public interface ICart
    {
        double Add(IProduct product);

        double Remove(IProduct product, int count);

        double Checkout();
    }
}