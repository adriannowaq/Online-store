using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.Home
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
