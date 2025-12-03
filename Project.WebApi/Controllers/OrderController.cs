using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Bll.Dtos;
using Project.Bll.Managers.Abstracts;
using Project.Bll.Results;
using Project.ViewModels.Models.RequestModels.Orders;
using Project.ViewModels.Models.ResponseModels.Orders;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly IMapper _mapper;

        public OrderController(IOrderManager orderManager, IMapper mapper)
        {
            _orderManager = orderManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> OrdersList()
        {
            var result = await _orderManager.GetAllAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            List<OrderResponseModel> responseModel = _mapper.Map<List<OrderResponseModel>>(result.Data);
            return Ok(responseModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var result = await _orderManager.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(_mapper.Map<OrderResponseModel>(result.Data));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequestModel model)
        {
            OrderDto order = _mapper.Map<OrderDto>(model);
            var result = await _orderManager.CreateAsync(order);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderRequestModel model)
        {
            OrderDto order = _mapper.Map<OrderDto>(model);
            var result = await _orderManager.UpdateAsync(order);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PacifyOrder(int id)
        {
            var result = await _orderManager.SoftDeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderManager.HardDeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}