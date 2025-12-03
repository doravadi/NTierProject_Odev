using Project.Entities.Enums;

namespace Project.ViewModels.Models.ResponseModels.OrderDetails
{
    public class OrderDetailResponseModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public DataStatus Status { get; set; }
    }
}