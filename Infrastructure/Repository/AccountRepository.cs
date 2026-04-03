using Application.Interface.Repository;
using Domain.Aggregate.Account;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankingAppContext _context;

        public AccountRepository(BankingAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public Task Delete(Account account)
        {
            _context.Accounts.Remove(account);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account?> GetByNumberAsync(string number)
        {
            return await _context.Accounts.Where(a => a.Number == number).FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
