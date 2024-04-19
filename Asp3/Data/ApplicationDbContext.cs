using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Asp3.Models;

namespace Asp3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Asp3.Models.book>? book { get; set; }
        public DbSet<Asp3.Models.borrowing>? borrowing { get; set; }
        public DbSet<Asp3.Models.reader>? reader { get; set; }
        public DbSet<Asp3.Models.reader>? Readers { get; set; }
    }
}
