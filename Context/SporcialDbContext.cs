using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SporcialAPI.Entities;

namespace SporcialAPI.Context;

public class SporcialDbContext : IdentityDbContext
{
    public SporcialDbContext(DbContextOptions<SporcialDbContext> options) : base(options)
    {
    }

    public DbSet<Activity> Activities { get; set; }
}
