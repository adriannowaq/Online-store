using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.Account
{
    public class AddressViewModel
    {
        public Address UserDetails { get; set; }
        public Address ShippingDetails { get; set; }
    }
}
