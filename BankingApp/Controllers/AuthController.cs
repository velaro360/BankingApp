using Application.Auth;
using Application.DTO;
using Application.User;
using AutoMapper;
using BankingApp.Controllers.Base;
using BankingApp.Request;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //To do: Add protection from attacks
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
            //Dodać zwracanie userId oraz zamienić string na DTO
            var authDto = _autoMapper.Map<AuthDTO>(request);
            var token = await _authService.AuthenticateAsync(authDto);
            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var registerDto = _autoMapper.Map<RegisterDTO>(request);
            await _authService.RegisterAsync(registerDto);
            return Ok("User registered successfully.");
        }
    }
}
