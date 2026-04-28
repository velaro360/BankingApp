using System.ComponentModel.DataAnnotations;

namespace BankingApp.Request
{
    public record TransferRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int FromAccountId { get; init; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ToAccountId { get; init; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Amount { get; init; }
    }
}
