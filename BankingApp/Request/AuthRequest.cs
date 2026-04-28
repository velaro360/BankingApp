using System.ComponentModel.DataAnnotations;

namespace BankingApp.Request
{
    public record AuthRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; init; } = string.Empty;
    }
}
