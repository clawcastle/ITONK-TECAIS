using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.AccountingControl.Models;

namespace AccountingControl.Data
{
    public class AccountingContext : DbContext
    {
        public AccountingContext(DbContextOptions<AccountingContext> options) : base(options) { }

        public DbSet<AccountingInformation> Billings { get; set; }
        public DbSet<HouseholdModel> Households { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountingInformation>().ToTable("AccountingInformation");
            modelBuilder.Entity<HouseholdModel>().ToTable("HouseholdModel");
        }
    }
}
