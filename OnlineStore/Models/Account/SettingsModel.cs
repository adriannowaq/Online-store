using OnlineStore.Data;

namespace OnlineStore.Models.Account
{
    public class SettingsModel
    {

        public SettingsModel() {}

        public SettingsModel(Address details)
        {
            if (details != null)
            {
                this.Name = details.Name;
                this.Surname = details.Surname;
                this.Phone = details.Phone;
                this.Street = details.Street;
                this.BuildingNumber = details.BuildingNumber;
                this.LocalNumber = details.LocalNumber;
                this.City = details.City;
                this.PostCode = details.PostCode;
                this.AddressType = details.AddressType;
            }
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string LocalNumber { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public AddressType AddressType { get; set; }
    }
}
