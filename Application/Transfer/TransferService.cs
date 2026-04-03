using Application.DTO;
using Application.Interface.Repository;
using Domain.Aggregate.ValueObject;

namespace Application.Transfer
{
    public class TransferService : ITransferService
    {
        private readonly IAccountRepository _accountRepository;

        public TransferService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task TransferAsync(TransferDTO transfer)
        {
            var fromAccount = await _accountRepository.GetByIdAsync(transfer.FromAccountId);
            var toAccount = await _accountRepository.GetByIdAsync(transfer.ToAccountId);

            if(fromAccount == null || toAccount == null)
            {
                throw new Exception("One or both accounts not found.");
            }

            //To do: Sprawdzać waluty i robić konwersję, jeśli są różne
            fromAccount.Withdraw(new MoneyVO(transfer.Amount, fromAccount.Currency));
            toAccount.Deposit(new MoneyVO(transfer.Amount, toAccount.Currency));

            await _accountRepository.SaveChangesAsync();
        }
    }
}
