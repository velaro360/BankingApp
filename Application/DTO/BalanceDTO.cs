using Domain.Enum;

namespace Application.DTO
{
    public class BalanceDTO
    {
        public decimal Amount { get; set; }
        public CurrencyEnum Currency { get; set; }
    }
}
