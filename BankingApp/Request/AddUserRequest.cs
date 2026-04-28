using System.ComponentModel.DataAnnotations;

namespace BankingApp.Request
{
    public record AddUserRequest
    {
        [Required]
        [MaxLength(60)]
        public string FirstName { get; init; } = string.Empty;
        [Required]
        [MaxLength(60)]
        public string LastName { get; init; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Street { get; init; } = string.Empty;

        [MaxLength(20)]
        public string FlatNumber { get; init; } = string.Empty;

        [Required]
        [MaxLength(60)]
        public string City { get; init; } = string.Empty;

        [Required]
        [MaxLength(60)]
        public string Country { get; init; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string ZipCode { get; init; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
    }
}
