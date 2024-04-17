using System.ComponentModel.DataAnnotations;

namespace Asp3.Models
{
    public class borrowing
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int ReaderId { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}
