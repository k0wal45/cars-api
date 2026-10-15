using Cars.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cars.Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<Car> Cars { get; set; }
}