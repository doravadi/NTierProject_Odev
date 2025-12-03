using Project.Entities.Enums;

namespace Project.ViewModels.Models.ResponseModels.Products
{
    public class ProductResponseModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int? CategoryId { get; set; }
        public DataStatus Status { get; set; }
    }
}