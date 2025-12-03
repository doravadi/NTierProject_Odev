using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Bll.Dtos;
using Project.Bll.Managers.Abstracts;
using Project.Bll.Results;
using Project.ViewModels.Models.RequestModels.Products;
using Project.ViewModels.Models.ResponseModels.Products;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly IMapper _mapper;

        public ProductController(IProductManager productManager, IMapper mapper)
        {
            _productManager = productManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ProductsList()
        {
            var result = await _productManager.GetAllAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            List<ProductResponseModel> responseModel = _mapper.Map<List<ProductResponseModel>>(result.Data);
            return Ok(responseModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _productManager.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(_mapper.Map<ProductResponseModel>(result.Data));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequestModel model)
        {
            ProductDto product = _mapper.Map<ProductDto>(model);
            var result = await _productManager.CreateAsync(product);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequestModel model)
        {
            ProductDto product = _mapper.Map<ProductDto>(model);
            var result = await _productManager.UpdateAsync(product);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PacifyProduct(int id)
        {
            var result = await _productManager.SoftDeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productManager.HardDeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}