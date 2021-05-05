using Microsoft.EntityFrameworkCore;
using SomeVueJSApp.Data.Models;

namespace SomeVueJSApp.Data
{
    public class DataTableContext : DbContext
    {
        public DataTableContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
