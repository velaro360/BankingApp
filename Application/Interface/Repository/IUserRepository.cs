using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregate.User;

namespace Application.Interface.Repository
{
    public interface IUserRepository
    {
        Task<Domain.Aggregate.User.User?> GetUserByIdAsync(int id);
        Task AddUserAsync(Domain.Aggregate.User.User user);
        Task DeleteUser(Domain.Aggregate.User.User user);
        Task<IEnumerable<Domain.Aggregate.User.User>> GetAllUsersAsync();
        Task SaveChangesAsync();
    }
}
