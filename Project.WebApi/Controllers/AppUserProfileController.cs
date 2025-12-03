using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Bll.Dtos;
using Project.Bll.Managers.Abstracts;
using Project.Bll.Results;
using Project.ViewModels.Models.RequestModels.AppUserProfiles;
using Project.ViewModels.Models.ResponseModels.AppUserProfiles;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserProfileController : ControllerBase
    {
        private readonly IAppUserProfileManager _appUserProfileManager;
        private readonly IMapper _mapper;

        public AppUserProfileController(IAppUserProfileManager appUserProfileManager, IMapper mapper)
        {
            _appUserProfileManager = appUserProfileManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> AppUserProfilesList()
        {
            var result = await _appUserProfileManager.GetAllAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            List<AppUserProfileResponseModel> responseModel = _mapper.Map<List<AppUserProfileResponseModel>>(result.Data);
            return Ok(responseModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppUserProfile(int id)
        {
            var result = await _appUserProfileManager.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(_mapper.Map<AppUserProfileResponseModel>(result.Data));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUserProfile(CreateAppUserProfileRequestModel model)
        {
            AppUserProfileDto profile = _mapper.Map<AppUserProfileDto>(model);
            var result = await _appUserProfileManager.CreateAsync(profile);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppUserProfile(UpdateAppUserProfileRequestModel model)
        {
            AppUserProfileDto profile = _mapper.Map<AppUserProfileDto>(model);
            var result = await _appUserProfileManager.UpdateAsync(profile);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PacifyAppUserProfile(int id)
        {
            var result = await _appUserProfileManager.SoftDeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAppUserProfile(int id)
        {
            var result = await _appUserProfileManager.HardDeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}