using Domain.Aggregate.Account;
using Domain.Aggregate.User;
using Domain.Aggregate.ValueObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class BankingAppContext : DbContext
    {
        public BankingAppContext(DbContextOptions<BankingAppContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(builder =>
            {
                builder.OwnsOne(x => x.Balance, money =>
                {
                    money.Property(x => x.Amount).HasColumnName("BalanceAmount").HasPrecision(18,2);
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
    }
}
