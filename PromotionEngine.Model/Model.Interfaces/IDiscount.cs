using System;

namespace PromotionEngine.Model.Model.Interfaces
{
    public interface IDiscount
    {
        Guid Id { get; set; }
        bool IsActive { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        string Title { get; set; }
        string Description { get; set; }
    }
}
