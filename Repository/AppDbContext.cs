using Microsoft.EntityFrameworkCore;
using System;

namespace WebApplication2.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Model.Product> Products => Set<Model.Product>();
    }
}
