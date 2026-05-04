using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account
{
    public interface IAccountService
    {
        Task<AccountDTO> GetAsync(int accountId, int currentUserId);
        Task<List<AccountDTO>> GetListAsync(int userId);
        Task AddAsync(AccountDTO request, int currentUserId);
        Task DeleteAsync(int accountId, int currentUserId);
        Task<BalanceDTO> GetBalanceAsync(int accountId, int currentUserId);
    }
}
