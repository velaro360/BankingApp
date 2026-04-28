using Domain.Enum;

namespace Domain.Aggregate.ValueObject
{
    public record MoneyVO
    {
        private MoneyVO() { }
        public MoneyVO(decimal amount, CurrencyEnum curr)
        {
            if(amount < 0)
                throw new ArgumentException("Amount cannot be negative.", nameof(amount));
            Amount = amount;
            Currency = curr;
        }

        public MoneyVO(decimal amount) : this(amount, CurrencyEnum.USD) { }

        public decimal Amount { get; init; }
        public CurrencyEnum Currency { get; init; }
    }
}
