using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControlWork9.Models;

public class Context : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public Context(DbContextOptions opts) : base(opts)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Transaction>()
            .HasOne(t => t.UserFrom)
            .WithMany(t => t.SendTransactions)
            .HasForeignKey(t => t.UserFromId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Transaction>()
            .HasOne(t => t.UserTo)
            .WithMany(t => t.ReceivedTransactions)
            .HasForeignKey(t => t.UserToId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
