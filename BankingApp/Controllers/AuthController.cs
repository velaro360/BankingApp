using Application.Auth;
using Application.DTO;
using AutoMapper;
using BankingApp.Request;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _autoMapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _autoMapper = mapper;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthRequest request)
        {
            var authDto = _autoMapper.Map<AuthDTO>(request);
            var token = await _authService.AuthenticateAsync(authDto);
            return Ok(token);
        }
    }
}
