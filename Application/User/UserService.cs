using Application.DTO;
using Application.Interface.Repository;
using AutoMapper;
using Domain.Aggregate.ValueObject;
using UserAggr = Domain.Aggregate.User;

namespace Application.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _autoMapper;

        public UserService(IUserRepository userRepository, IMapper autoMapper)
        {
            _userRepository = userRepository;
            _autoMapper = autoMapper;
        }

        public async Task AddAsync(UserDTO user)
        {
            var userEntity = new UserAggr.User(
                user.FirstName,
                user.LastName,
                new AddressVO(
                    user.Street,
                    user.FlatNumber,
                    user.City,
                    user.Country,
                    user.ZipCode),
                user.Email);

            await _userRepository.AddUserAsync(userEntity);

            await _userRepository.SaveChangesAsync();
        }

        public async Task<UserDTO> GetAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if(user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            var userDTO = _autoMapper.Map<UserDTO>(user);

            return userDTO;
        }
    }
}
