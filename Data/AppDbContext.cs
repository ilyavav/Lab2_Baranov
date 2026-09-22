using Lab2_Baranov.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2_Baranov.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; } = default!;

        public DbSet<Car> Cars { get; set; } = default!;

        public DbSet<RepairOrder> RepairOrders { get; set; } = default!;
    }
}