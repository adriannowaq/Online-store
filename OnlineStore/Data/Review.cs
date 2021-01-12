using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Data
{
    public class Review
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Date { get; set; }
        public string AuthorName { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
