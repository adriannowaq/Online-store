using OnlineStore.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Cart
{
    public class AddAddressModel
    {
        public AddAddressModel() {}
        public AddAddressModel(Address address)
        {
            if (address != null)
            {
                this.Name = address.Name;
                this.Surname = address.Surname;
                this.Phone = address.Phone;
                this.Street = address.Street;
                this.BuildingNumber = address.BuildingNumber;
                this.LocalNumber = address.LocalNumber;
                this.City = address.City;
                this.PostCode = address.PostCode;
            }
        }
        public AddAddressModel(Order order)
        {
            if (order != null)
            {
                this.Name = order.Name;
                this.Surname = order.Surname;
                this.Phone = order.Phone;
                this.Street = order.Street;
                this.BuildingNumber = order.BuildingNumber;
                this.LocalNumber = order.LocalNumber;
                this.City = order.City;
                this.PostCode = order.PostCode;
            }
        }

        [Required(ErrorMessage = "Pole wymagane.")]
        [Range(0, 0)]
        public PaymentOption PaymentMethod { get; set; }

        [Required(ErrorMessage = "Pole wymagane.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole wymagane.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Pole wymagane.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Pole wymagane.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Pole wymagane.")]
        public string BuildingNumber { get; set; }

        public string LocalNumber { get; set; }

        [Required(ErrorMessage = "Pole wymagane.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Pole wymagane.")]
        public string PostCode { get; set; }
    }
}
