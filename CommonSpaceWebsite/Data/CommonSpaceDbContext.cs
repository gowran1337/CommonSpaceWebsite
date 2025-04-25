using CommonSpaceWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace CommonSpaceWebsite.Data
{
    public class CommonSpaceDbContext : DbContext
    {
        public CommonSpaceDbContext(DbContextOptions<CommonSpaceDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CleaningTask> CleaningTasks { get; set; }
        public DbSet<CleaningWeek> CleaningWeeks { get; set; }
        public DbSet<CleaningWeekTask> CleaningWeekTasks { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. User to CleaningWeek (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.AssignedWeeks)          // A user can have many assigned weeks
                .WithOne(w => w.Cleaner)                // Each week has one cleaner (user)
                .HasForeignKey(w => w.CleanerId)        // Foreign key in CleaningWeek table
                .OnDelete(DeleteBehavior.Restrict);     // Prevent user deletion if they have assigned weeks

            // 2. CleaningWeek to CleaningWeekTask (One-to-Many)
            modelBuilder.Entity<CleaningWeek>()
                .HasMany(w => w.Tasks)                 // A week has many tasks
                .WithOne(t => t.Week)                  // Each task belongs to one week
                .HasForeignKey(t => t.WeekId)           // Foreign key in CleaningWeekTask
                .OnDelete(DeleteBehavior.Cascade);      // Delete tasks when week is deleted

            // 3. CleaningTask to CleaningWeekTask (One-to-Many)
            modelBuilder.Entity<CleaningTask>()
                .HasMany(t => t.WeekTasks)             // A standard task can appear in many weeks
                .WithOne(wt => wt.Task)                // Each week-task links to one standard task
                .HasForeignKey(wt => wt.TaskId)         // Foreign key in CleaningWeekTask
                .OnDelete(DeleteBehavior.Restrict);     // Prevent deletion if task is assigned to weeks

            // 4. User to ShoppingItem (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.PurchasedItems)        // A user can purchase many items
                .WithOne(i => i.PurchasedBy)           // Each item is purchased by one user
                .HasForeignKey(i => i.PurchasedById)    // Foreign key in ShoppingItem
                .OnDelete(DeleteBehavior.SetNull);      // If user is deleted, keep items but set buyer to null

            // 5. CleaningWeek to ShoppingItem (One-to-Many)

             modelBuilder.Entity<ShoppingItem>()
                .HasOne(s => s.PurchasedBy)   // A week can have many shopping items
                .WithMany(u => u.PurchasedItems)      // ShoppingItem doesn't need navigation back
                .HasForeignKey(s => s.PurchasedById)   // Foreign key in ShoppingItem
                .OnDelete(DeleteBehavior.SetNull); // Delete items when week is deleted

        }

    }
}
