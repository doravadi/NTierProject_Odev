using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Bll.Dtos;
using Project.Bll.Managers.Abstracts;
using Project.Bll.Results;
using Project.ViewModels.Models.RequestModels.AppUsers;
using Project.ViewModels.Models.ResponseModels.AppUsers;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IMapper _mapper;

        public AppUserController(IAppUserManager appUserManager, IMapper mapper)
        {
            _appUserManager = appUserManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> AppUsersList()
        {
            var result = await _appUserManager.GetAllAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            List<AppUserResponseModel> responseModel = _mapper.Map<List<AppUserResponseModel>>(result.Data);
            return Ok(responseModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppUser(int id)
        {
            var result = await _appUserManager.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(_mapper.Map<AppUserResponseModel>(result.Data));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUser(CreateAppUserRequestModel model)
        {
            AppUserDto appUser = _mapper.Map<AppUserDto>(model);
            var result = await _appUserManager.CreateAsync(appUser);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppUser(UpdateAppUserRequestModel model)
        {
            AppUserDto appUser = _mapper.Map<AppUserDto>(model);
            var result = await _appUserManager.UpdateAsync(appUser);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PacifyAppUser(int id)
        {
            var result = await _appUserManager.SoftDeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAppUser(int id)
        {
            var result = await _appUserManager.HardDeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}