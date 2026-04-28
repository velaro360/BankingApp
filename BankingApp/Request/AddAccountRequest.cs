using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.Request
{
    public record AddAccountRequest
    {
        [Required]
        [EnumDataType(typeof(CurrencyEnum))]
        public CurrencyEnum Currency { get; init; }
    }
}
