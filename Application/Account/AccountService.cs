using Application.DTO;
using Application.Interface.Repository;
using AutoMapper;
using AccountAggr = Domain.Aggregate.Account;

namespace Application.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _autoMapper;

        public AccountService(IAccountRepository accountRepository, IMapper autoMapper)
        {
            _accountRepository = accountRepository;
            _autoMapper = autoMapper;
        }

        public async Task AddAsync(AccountDTO request)
        {
            var account =
                new AccountAggr.Account(request.OwnerId, GenerateAccountNumber(), request.Currency);

            await _accountRepository.AddAsync(account);

            await _accountRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            if(account == null)
                throw new Exception($"Account with id {accountId} not found.");

            _accountRepository.Delete(account);

            await _accountRepository.SaveChangesAsync();
        }

        public async Task<AccountDTO> GetAsync(int accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            if (account == null)
                throw new Exception($"Account with id {accountId} not found.");

            return _autoMapper.Map<AccountDTO>(account);
        }

        public async Task<BalanceDTO> GetBalanceAsync(int accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            if(account == null)
                throw new Exception($"Account with id {accountId} not found.");

            var balanceDTO = _autoMapper.Map<BalanceDTO>(account.Balance);
            return balanceDTO;
        }

        public async Task<List<AccountDTO>> GetListAsync(int userId)
        {
            var accounts = await _accountRepository.GetAllAsync();
            var userAccounts = accounts.Where(a => a.OwnerId == userId).ToList();

            return _autoMapper.Map<List<AccountDTO>>(userAccounts);
        }

        //To do: Sprawdzać czy generowany numer konta jest unikalny
        private string GenerateAccountNumber()
        {
            Random randomizer = new Random();

            string accNumber = "1220040000";

            for(int i=0; i < 4; i++)
                accNumber += randomizer.Next(1000, 9999).ToString();

            return accNumber;
        }
    }
}
