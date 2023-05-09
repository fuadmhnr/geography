using Geography.Models;
using Microsoft.EntityFrameworkCore;

namespace Geography.Data;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options)
  {
  }

  public DbSet<Country>? Countries { get; set; }
  public DbSet<Province>? Provinces { get; set; }
}