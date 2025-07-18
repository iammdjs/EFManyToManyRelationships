
using EFManyToMany.Models;
using Microsoft.EntityFrameworkCore;

namespace EFManyToMany.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
