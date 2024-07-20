using Microsoft.EntityFrameworkCore;
using PTGKDotNetCoreBath4.WebApi.Models;

namespace PTGKDotNetCoreBath4.WebApi.Db
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }
       public DbSet<CustomerModel> Customer {  get; set; }
    }
}
