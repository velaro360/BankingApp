using Domain.Aggregate.ValueObject.Abstraction;

namespace Domain.Aggregate.ValueObject
{
    public class AddressVO : ValueObjectBase
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

        public string Street { get; private set; }
        public string FlatNumber { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public override bool IsIdentical(ValueObjectBase other)
        {
            if (other is not AddressVO otherAddress)
                return false;

            return Street == otherAddress.Street &&
                   FlatNumber == otherAddress.FlatNumber &&
                   City == otherAddress.City &&
                   Country == otherAddress.Country &&
                   ZipCode == otherAddress.ZipCode;
        }
    }
}
