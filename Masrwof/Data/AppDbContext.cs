using Masrwof.Models;
using Microsoft.EntityFrameworkCore;

namespace Masrwof.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
    }
}