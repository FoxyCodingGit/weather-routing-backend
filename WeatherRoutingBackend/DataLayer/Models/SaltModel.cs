using System.ComponentModel.DataAnnotations;

namespace WeatherRoutingBackend.DataLayer.Models
{
    public class SaltModel // using EF to executre soitred proc has been dopne awfully. 
    {
        [Key]
        public string UserId { get; set; }
        public string Salt { get; set; }
    }
}
