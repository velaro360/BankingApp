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
        Task<DTO.DTO> GetAsync(int accountId, int currentUserId);
        Task<List<DTO.DTO>> GetListAsync(int userId);
        Task AddAsync(DTO.DTO request, int currentUserId);
        Task DeleteAsync(int accountId, int currentUserId);
        Task<BalanceDTO> GetBalanceAsync(int accountId, int currentUserId);
    }
}
