using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverEventHub.Infrastructure.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
