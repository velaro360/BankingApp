
namespace Domain.Aggregate.ValueObject
{
    public record AddressVO
    {
        private AddressVO() { }
        public AddressVO(string street, string flatNumber, string city, string country, string zipCode)
        {
            Street = street;
            FlatNumber = flatNumber;
            City = city;
            Country = country;
            ZipCode = zipCode;
        }

        public string Street { get; init; }
        public string FlatNumber { get; init; }
        public string City { get; init; }
        public string Country { get; init; }
        public string ZipCode { get; init; }
    }
}
