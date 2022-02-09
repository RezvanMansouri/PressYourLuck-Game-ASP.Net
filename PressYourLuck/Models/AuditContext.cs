using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AS3_RM5831.Models
{
    public class AuditContext : DbContext
    {
        public AuditContext(DbContextOptions<AuditContext> options)
            : base(options)
        {
        }

        public DbSet<Audit> Audits { get; set; }
        public DbSet<AuditType> AuditTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditType>().HasData(
                new AuditType()
                {
                    AuditTypeId = "CashIn",
                    Name = "Cash In"
                },
                new AuditType()
                {
                    AuditTypeId = "CashOut",
                    Name = "Cash Out"
                },
                new AuditType()
                {
                    AuditTypeId = "Win",
                    Name = "Win"
                },
                new AuditType()
                {
                    AuditTypeId = "Lose",
                    Name = "Lose"
                }
                );
        }
    }
}
