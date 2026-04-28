
namespace Application.Interface.Repository
{
    public interface IUserRepository
    {
        Task<Domain.Aggregate.User.User?> GetUserByIdAsync(int id);
        Task<Domain.Aggregate.User.User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(Domain.Aggregate.User.User user);
        Task DeleteUser(Domain.Aggregate.User.User user);
        Task<IEnumerable<Domain.Aggregate.User.User>> GetAllUsersAsync();
        Task SaveChangesAsync();
    }
}
