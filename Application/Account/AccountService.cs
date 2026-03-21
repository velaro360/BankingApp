using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account
{
    public class AccountService : IAccountService
    {
        public Task AddAsync(AccountDTO request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<AccountDTO> GetAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<BalanceDTO> GetBalanceAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountDTO>> GetListAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
