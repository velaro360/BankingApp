using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User
{
    public class UserService : IUserService
    {
        public Task AddAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
