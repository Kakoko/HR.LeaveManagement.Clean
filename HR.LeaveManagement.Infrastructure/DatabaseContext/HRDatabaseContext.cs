using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.DatabaseContext
{
    public class HRDatabaseContext : DbContext
    {
        public HRDatabaseContext(DbContextOptions<HRDatabaseContext> options) : base(options)
        {
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveRequests> LeaveRequests { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRDatabaseContext).Assembly);

            //There is a better way
            //modelBuilder.Entity<LeaveType>().HasData(new LeaveType { Id = 1, Name = "Vacation", DefaultDays = 10 , DateModified = DateTime.Now , DateCreated = DateTime.Now });
            //modelBuilder.Entity<LeaveType>().HasData(new LeaveType { Id = 2, Name = "Sick", DefaultDays = 10, DateModified = DateTime.Now, DateCreated = DateTime.Now });
            base.OnModelCreating(modelBuilder); 
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
