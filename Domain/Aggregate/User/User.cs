using Domain.Aggregate.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregate.Account;

namespace Domain.Aggregate.User
{
    public class User : DatabaseEntity
    {
        private User() { }
        public User(string firstName, string lastName, AddressVO address, string email)
        {
            if(string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
            FirstName = firstName;

            if(string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
            LastName = lastName;

            if(address == null)
                throw new ArgumentNullException(nameof(address));
            Address = address;

            if(string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            Email = email;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public AddressVO Address { get; private set; }
        public string Email { get; private set; }

        public void UpdateAddress(AddressVO newAddress)
        {
            if (newAddress == null)
                throw new ArgumentNullException(nameof(newAddress));
            Address = newAddress;
        }

        public void UpdateEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email cannot be null or empty.", nameof(newEmail));
            Email = newEmail;
        }

        public void UpdateName(string newFirstName, string newLastName)
        {
            if (string.IsNullOrWhiteSpace(newFirstName))
                throw new ArgumentException("First name cannot be null or empty.", nameof(newFirstName));
            if (string.IsNullOrWhiteSpace(newLastName))
                throw new ArgumentException("Last name cannot be null or empty.", nameof(newLastName));
            FirstName = newFirstName;
            LastName = newLastName;
        }

    }
}
