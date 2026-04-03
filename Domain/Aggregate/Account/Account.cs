using Domain.Aggregate.ValueObject;
using Domain.Enum;

namespace Domain.Aggregate.Account
{
    public class Account : DatabaseEntity
    {
        private Account() { }
        public Account(int ownerId, string number, CurrencyEnum currency)
        {
            if(ownerId <= 0)
                throw new ArgumentException("Owner ID must be greater than zero.", nameof(ownerId));
            OwnerId = ownerId;

            if(string.IsNullOrEmpty(number))
                throw new ArgumentException("Account number cannot be null or empty.", nameof(number));

            if(number.Length != 26)
                throw new ArgumentException("Account number must be 26 characters long.", nameof(number));

            Currency = currency;
            Number = number;
            IsActive = true;
            Balance = new MoneyVO(0, currency);
        }

        public int OwnerId { get; private set; }
        public string Number { get; private set; }
        public MoneyVO Balance { get; private set; }
        public bool IsActive { get; private set; }
        public CurrencyEnum Currency { get; private set; }


        //Dodać sprawdzanie waluty przed wpłatą
        public void Deposit(MoneyVO amount)
        {
            if (amount.Amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            Balance = new MoneyVO(Balance.Amount + amount.Amount, Balance.Currency);
        }

        //Dodać sprawdzanie waluty przed wypłatą
        public void Withdraw(MoneyVO amount)
        {
            if (amount.Amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");
            if (Balance.Amount < amount.Amount)
                throw new InvalidOperationException("Insufficient funds for withdrawal.");
            Balance = new MoneyVO(Balance.Amount - amount.Amount, Balance.Currency);
        }

        public void SetAccountNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
                throw new ArgumentException("Account number cannot be null or empty.", nameof(number));
            if (number.Length != 26)
                throw new ArgumentException("Account number must be 26 characters long.", nameof(number));
            Number = number;
        }

        public void Close()
        {
            if (Balance.Amount != 0)
                throw new InvalidOperationException("Account balance must be zero to close the account.");
            IsActive = false;
        }

        public void Reopen()
        {
            if (IsActive)
                throw new InvalidOperationException("Account is already active.");
            IsActive = true;
        }
    }
}
