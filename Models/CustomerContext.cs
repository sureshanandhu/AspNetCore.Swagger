using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Swagger.WebApi.Models
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Customer> customers { get; set; }
    }
}