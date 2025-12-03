using AutoMapper;
using Project.Bll.Dtos;
using Project.ViewModels.Models.RequestModels.AppUserProfiles;
using Project.ViewModels.Models.RequestModels.AppUsers;
using Project.ViewModels.Models.RequestModels.Categories;
using Project.ViewModels.Models.RequestModels.OrderDetails;
using Project.ViewModels.Models.RequestModels.Orders;
using Project.ViewModels.Models.RequestModels.Products;
using Project.ViewModels.Models.ResponseModels.AppUserProfiles;
using Project.ViewModels.Models.ResponseModels.AppUsers;
using Project.ViewModels.Models.ResponseModels.Categories;
using Project.ViewModels.Models.ResponseModels.OrderDetails;
using Project.ViewModels.Models.ResponseModels.Orders;
using Project.ViewModels.Models.ResponseModels.Products;

namespace Project.WebApi.MappingProfiles
{
    public class VmMappingProfile : Profile
    {
        public VmMappingProfile()
        {
            // Category mappings
            CreateMap<CreateCategoryRequestModel, CategoryDto>();
            CreateMap<UpdateCategoryRequestModel, CategoryDto>();
            CreateMap<CategoryDto, CategoryResponseModel>();

            // Product mappings
            CreateMap<CreateProductRequestModel, ProductDto>();
            CreateMap<UpdateProductRequestModel, ProductDto>();
            CreateMap<ProductDto, ProductResponseModel>();

            // AppUser mappings
            CreateMap<CreateAppUserRequestModel, AppUserDto>();
            CreateMap<UpdateAppUserRequestModel, AppUserDto>();
            CreateMap<AppUserDto, AppUserResponseModel>();

            // AppUserProfile mappings
            CreateMap<CreateAppUserProfileRequestModel, AppUserProfileDto>();
            CreateMap<UpdateAppUserProfileRequestModel, AppUserProfileDto>();
            CreateMap<AppUserProfileDto, AppUserProfileResponseModel>();

            // Order mappings
            CreateMap<CreateOrderRequestModel, OrderDto>();
            CreateMap<UpdateOrderRequestModel, OrderDto>();
            CreateMap<OrderDto, OrderResponseModel>();

            // OrderDetail mappings
            CreateMap<CreateOrderDetailRequestModel, OrderDetailDto>();
            CreateMap<UpdateOrderDetailRequestModel, OrderDetailDto>();
            CreateMap<OrderDetailDto, OrderDetailResponseModel>();
        }
    }
}