using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Bll.Dtos;
using Project.Bll.Managers.Abstracts;
using Project.Bll.Results;
using Project.ViewModels.Models.RequestModels.OrderDetails;
using Project.ViewModels.Models.ResponseModels.OrderDetails;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailManager _orderDetailManager;
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailManager orderDetailManager, IMapper mapper)
        {
            _orderDetailManager = orderDetailManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetailsList()
        {
            var result = await _orderDetailManager.GetAllAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            List<OrderDetailResponseModel> responseModel = _mapper.Map<List<OrderDetailResponseModel>>(result.Data);
            return Ok(responseModel);
        }

        [HttpGet("{orderId}/{productId}")]
        public async Task<IActionResult> GetOrderDetail(int orderId, int productId)
        {
            // OrderDetail composite key kullandığı için GetById çalışmayabilir
            // Alternatif çözüm: GetAll ile filtreleme
            var allResult = await _orderDetailManager.GetAllAsync();

            if (!allResult.IsSuccess)
                return BadRequest(allResult.Message);

            OrderDetailDto value = allResult.Data?.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);

            if (value == null)
                return NotFound("Sipariş detayı bulunamadı");

            return Ok(_mapper.Map<OrderDetailResponseModel>(value));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailRequestModel model)
        {
            OrderDetailDto orderDetail = _mapper.Map<OrderDetailDto>(model);
            var result = await _orderDetailManager.CreateAsync(orderDetail);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailRequestModel model)
        {
            OrderDetailDto orderDetail = _mapper.Map<OrderDetailDto>(model);
            var result = await _orderDetailManager.UpdateAsync(orderDetail);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> DeleteOrderDetail(int orderId, int productId)
        {

            var allResult = await _orderDetailManager.GetAllAsync();
            if (!allResult.IsSuccess)
                return BadRequest(allResult.Message);

            var orderDetail = allResult.Data?.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);

            if (orderDetail == null)
                return NotFound("Sipariş detayı bulunamadı");

            var result = await _orderDetailManager.HardDeleteAsync(orderDetail.Id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}