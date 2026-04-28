using Application.DTO;
using Application.User;
using AutoMapper;
using BankingApp.Controllers.Base;
using BankingApp.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : BankingAppBaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _autoMapper;

        public UserController(IUserService userService, IMapper autoMapper)
        {
            _userService = userService;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromBody] AddUserRequest request)
        {
            var user = _autoMapper.Map<UserDTO>(request);
            await _userService.AddAsync(user);
            return Ok("User created successfully.");
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAsync()
        {
            var userId = GetCurrentUserId();
            var user = await _userService.GetAsync(userId);
            return Ok(user);
        }
    }
}
