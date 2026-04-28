using Application.DTO;
using Application.Interface.Repository;
using UserAggr = Domain.Aggregate.User;
using AddressVO = Domain.Aggregate.ValueObject.AddressVO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthSettings _authSettings;

        public AuthService(IUserRepository userRepository, AuthSettings authSettings)
        {
            _userRepository = userRepository;
            _authSettings = authSettings;
        }

        public async Task RegisterAsync(RegisterDTO registerDto)
        {
            var users = await _userRepository.GetAllUsersAsync();
            if (users.Any(u => u.Email == registerDto.Email))
            {
                throw new BadRequestException("User with specified email already exists");
            }

            UserAggr.User user = new UserAggr.User(registerDto.FirstName,
                registerDto.LastName,
                new AddressVO(registerDto.Street,
                    registerDto.FlatNumber,
                    registerDto.City,
                    registerDto.Country,
                    registerDto.ZipCode),
                registerDto.Email);


            var hasher = new PasswordHasher<UserAggr.User>();
            var passwordHash = hasher.HashPassword(user, registerDto.Password);

            user.SetPasswordHash(passwordHash);

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<string> AuthenticateAsync(AuthDTO authDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(authDto.Email);
            if (user is null)
            {
                throw new BadRequestException("Invalid email or password");
            }

            if (new PasswordHasher<UserAggr.User>().VerifyHashedPassword(user, user.PasswordHash, authDto.Password) == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authSettings.JwtIssuer,
                null,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
