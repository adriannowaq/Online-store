using System;

namespace OnlineStore.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public Cart Cart { get; set; }
    }
}
