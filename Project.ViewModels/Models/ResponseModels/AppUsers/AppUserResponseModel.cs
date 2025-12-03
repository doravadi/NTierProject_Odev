using Project.Entities.Enums;

namespace Project.ViewModels.Models.ResponseModels.AppUsers
{
    public class AppUserResponseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DataStatus Status { get; set; }
    }
}