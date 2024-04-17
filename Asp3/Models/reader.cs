using System.ComponentModel.DataAnnotations;

namespace Asp3.Models
{
    public class reader
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
