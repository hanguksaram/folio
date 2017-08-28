using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.SqlServer
{
    using System.Data.Entity;

    using ERP.Core.Models;
    using Migrations;

    public class Db : DbContext
    {
        public Db()
            : base("ERPContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<Db>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<GroupOfSkills> GroupOfSkills { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<HistoryEmployee> History { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(t => t.ID);
            modelBuilder.Entity<Company>().HasMany(c => c.Managers).WithMany(p => p.Companies);
            base.OnModelCreating(modelBuilder);
        }
    }
}
