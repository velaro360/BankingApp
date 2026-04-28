using Application.DTO;
using Application.Interface.Repository;
using Application.Middleware.Exceptions;
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

        public async Task AddAsync(DTO.DTO request, int currentUserId)
        {
            var account =
                new AccountAggr.Account(currentUserId, GenerateAccountNumber(), request.Currency);

            await _accountRepository.AddAsync(account);

            await _accountRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int accountId, int currentUserId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            if(account == null)
                throw new NotFoundException($"Account with id {accountId} not found.");
            if(account.OwnerId != currentUserId)
                throw new Exception($"User with id {currentUserId} is not the owner of the account with id {accountId}.");

            _accountRepository.Delete(account);

            await _accountRepository.SaveChangesAsync();
        }

        public async Task<DTO.DTO> GetAsync(int accountId, int currentUserId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            if (account == null)
                throw new NotFoundException($"Account with id {accountId} not found.");
            if(account.OwnerId != currentUserId)
                throw new ForbiddenOperationException($"User with id {currentUserId} is not the owner of the account with id {accountId}.");

            return _autoMapper.Map<DTO>(account);
        }

        public async Task<BalanceDTO> GetBalanceAsync(int accountId, int currentUserId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            if(account == null)
                throw new NotFoundException($"Account with id {accountId} not found.");
            if (account.OwnerId != currentUserId)
                throw new ForbiddenOperationException($"User with id {currentUserId} is not the owner of the account with id {accountId}.");

            var balanceDTO = _autoMapper.Map<BalanceDTO>(account.Balance);
            return balanceDTO;
        }

        public async Task<List<DTO.DTO>> GetListAsync(int userId)
        {
            var accounts = await _accountRepository.GetAllAsync();
            var userAccounts = accounts.Where(a => a.OwnerId == userId).ToList();

            return _autoMapper.Map<List<DTO>>(userAccounts);
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
