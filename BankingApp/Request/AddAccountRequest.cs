using Domain.Enum;

namespace BankingApp.Request
{
    public class AddAccountRequest
    {
        public int OwnerId { get; set; }
        public CurrencyEnum Currency { get; set; }
    }
}
