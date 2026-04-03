using Application.DTO;

namespace Application.User
{
    public interface IUserService
    {
        Task AddAsync(UserDTO user);
        Task<UserDTO> GetAsync(int userId);
    }
}
