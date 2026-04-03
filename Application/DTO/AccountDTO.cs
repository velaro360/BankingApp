using Domain.Enum;
using System;

namespace Application.DTO
{
    public class AccountDTO
    {
        public int OwnerId { get; set; }
        public string Number { get; set; }
        public decimal BalanceMoney { get; set; }
        public bool IsActive { get; set; }
        public CurrencyEnum Currency { get; set; }
    }
}
