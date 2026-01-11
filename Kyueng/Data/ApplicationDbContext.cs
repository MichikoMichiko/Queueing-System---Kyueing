using Microsoft.EntityFrameworkCore;
using Kyueng.Models;

namespace Kyueng.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<QueueTicket> QueueTickets { get; set; }
        public DbSet<QueueCall> QueueCalls { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<SystemStatus> SystemStatuses { get; set; }

     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Prevent deleting QueueCall when QueueTicket is deleted
            modelBuilder.Entity<QueueCall>()
                .HasOne(qc => qc.QueueTicket)
                .WithMany()
                .HasForeignKey(qc => qc.QueueTicketId)
                .OnDelete(DeleteBehavior.SetNull); // ✅ Keeps QueueCall history

            // ✅ Prevent deleting QueueTickets when Student is deleted
            modelBuilder.Entity<QueueTicket>()
                .HasOne(q => q.Student)
                .WithMany()
                .HasForeignKey(q => q.StudentId)
                .OnDelete(DeleteBehavior.SetNull); // ✅ Keeps QueueTicket history
        }

    }
}
