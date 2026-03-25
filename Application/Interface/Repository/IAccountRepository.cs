using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IAccountRepository
    {
        Task AddAsync(Domain.Aggregate.Account.Account account);
        Task<Domain.Aggregate.Account.Account?> GetByNumberAsync(string number);
        Task<Domain.Aggregate.Account.Account?> GetByIdAsync(int id);
        Task Delete(Domain.Aggregate.Account.Account account);
        Task<IEnumerable<Domain.Aggregate.Account.Account>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
