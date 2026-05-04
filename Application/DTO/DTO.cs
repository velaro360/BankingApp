using Domain.Enum;
using System;

namespace Application.DTO
{
    public record AccountDTO(CurrencyEnum Currency, int OwnerId=0, string Number="", decimal BalanceMoney=0, bool IsActive=false);

    public record UserDTO(string FirstName, string LastName, string Street, string FlatNumber, string City, string Country, string ZipCode, string Email);

    public record TransferDTO(int FromAccountId, int ToAccountId, decimal Amount);

    public record RegisterDTO(string FirstName, string LastName, string Street, string FlatNumber, string City, string Country, string ZipCode, string Email, string Password);

    public record BalanceDTO(decimal Amount, CurrencyEnum Currency);

    public record AuthDTO(string Email, string Password);
}
