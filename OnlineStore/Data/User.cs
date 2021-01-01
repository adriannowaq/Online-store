using System.Collections.Generic;

namespace OnlineStore.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
    public enum UserRole
    {
        None,
        User,
        Admin
    }
}
