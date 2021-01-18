using OnlineStore.Models.Account;

namespace OnlineStore.Data
{
    public enum AddressType
    {
        Address,
        DeliveryAddress
    }
    public class Address
    {
        public Address() {}

        public Address(SettingsModel details)
        {
            this.Name = details.Name;
            this.Surname = details.Surname;
            this.Phone = details.Phone;
            this.Street = details.Street;
            this.BuildingNumber = details.BuildingNumber;
            this.LocalNumber = details.LocalNumber;
            this.City = details.City;
            this.PostCode = details.PostCode;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string LocalNumber { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public AddressType AddressType { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
