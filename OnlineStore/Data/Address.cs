﻿namespace OnlineStore.Data
{
    public enum AddressType
    {
        Address,
        DeliveryAddress
    }
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string LocalNumber { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public AddressType AddressType { get; set; }
        public User User { get; set; }
    }
}