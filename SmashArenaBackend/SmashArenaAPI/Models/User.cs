using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SmashArenaAPI.Models
{
    [Table("Users")] 
    public class User
    {
        [Key]
        [StringLength(10)]
        public string UserId { get; set; } 

        public string? NamaLengkap { get; set; } 

        public string Email { get; set; }

        public string Password { get; set; }

        public string? NomorHp { get; set; }

        public string? Role { get; set; }

        [JsonIgnore]
        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}