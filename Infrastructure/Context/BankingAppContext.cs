using Domain;
using Domain.Aggregate.Account;
using Domain.Aggregate.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class BankingAppContext : DbContext
    {
        public BankingAppContext(DbContextOptions<BankingAppContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public override int SaveChanges()
        {
            UpdateAuditTimestamps();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateAuditTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(builder =>
            {
                builder.OwnsOne(x => x.Balance, money =>
                {
                    money.Property(x => x.Amount).HasColumnName("BalanceAmount").HasPrecision(18, 2);
                    money.Property(x => x.Currency).HasColumnName("BalanceCurrency");
                });
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.OwnsOne(x => x.Address, address =>
                {
                    address.Property(x => x.Street).HasColumnName("Street");
                    address.Property(x => x.FlatNumber).HasColumnName("FlatNumber");
                    address.Property(x => x.City).HasColumnName("City");
                    address.Property(x => x.Country).HasColumnName("Country");
                    address.Property(x => x.ZipCode).HasColumnName("ZipCode");
                });
            });
        }

        private void UpdateAuditTimestamps()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<DatabaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(entity => entity.CreatedAt).CurrentValue = now;
                    entry.Property(entity => entity.UpdatedAt).CurrentValue = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(entity => entity.CreatedAt).IsModified = false;
                    entry.Property(entity => entity.UpdatedAt).CurrentValue = now;
                }
            }
        }
    }
}
