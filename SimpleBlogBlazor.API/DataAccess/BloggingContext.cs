using Microsoft.EntityFrameworkCore;
using SimpleBlogBlazor.Shared.Models;

namespace SimpleBlogBlazor.API.DataAccess
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
        : base(options) { }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
