namespace PromotionEngine.Model.Model.Interfaces
{
    public interface IProductBasedDiscount : IDiscount
    {
        double Apply(int productCount, double productPrice);
    }
}
