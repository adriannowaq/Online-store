using OnlineStore.Models.Cart;
using System;
using System.Collections.Generic;

namespace OnlineStore.Data
{
    public enum PaymentOption
    {
        OnDelivery
    }
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public PaymentOption PaymentOption { get; set; }
        public bool Paid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string LocalNumber { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public DateTimeOffset Date { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public bool Completed { get; set; }
        public bool AddressesCompleted { get; set; }
        public int CartId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
