using Domain.Aggregate.ValueObject.Abstraction;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregate.ValueObject
{
    public class MoneyVO : ValueObjectBase
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

        public decimal Amount { get; private set; }
        public CurrencyEnum Currency { get; private set; }

        public override bool IsIdentical(ValueObjectBase other)
        {
            if (other is not MoneyVO otherMoney)
                return false;

            return Amount == otherMoney.Amount && Currency == otherMoney.Currency;
        }
    }
}
