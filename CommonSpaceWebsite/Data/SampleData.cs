using CommonSpaceWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace CommonSpaceWebsite.Data
{
    public static class SampleData
    {
        public static void Initialize(CommonSpaceDbContext context)
        {
            // Ensure the database is created and migrations are applied
            context.Database.EnsureCreated();

            // Check if database already has data
            if (context.Users.Any() || context.CleaningTasks.Any())
            {
                return; // Database already seeded
            }

            // Add sample users
            //var users = new User[]
            //{
            //    new User { Name = "John Doe", Password = "password123" },
            //    new User { Name = "Jane Smith", Password = "letmein" },
            //    new User { Name = "Bob Johnson", Password = "securepw" }
            //};
            //context.Users.AddRange(users);
            //context.SaveChanges();

            // Add sample cleaning tasks
            var tasks = new CleaningTask[]
            {
                new CleaningTask { Name = "Vacuum hallway", Description = "Vacuum all common area hallways" },
                new CleaningTask { Name = "Clean kitchen", Description = "Wipe counters and clean appliances" },
                new CleaningTask { Name = "Take out trash", Description = "Collect all trash from common areas" }
            };
            context.CleaningTasks.AddRange(tasks);
            context.SaveChanges();

            // Add sample cleaning week
            //var currentWeek = new CleaningWeek
            //{
            //    StartDate = DateTime.Today,
            //    EndDate = DateTime.Today.AddDays(7),
            //    Cleaner = users[0] // Assign first user as cleaner
            //};
            //context.CleaningWeeks.Add(currentWeek);
            //context.SaveChanges();

            // Assign tasks to the week
            //    var weekTasks = new CleaningWeekTask[]
            //    {
            //        new CleaningWeekTask { WeekId = currentWeek.Id, TaskId = tasks[0].Id },
            //        new CleaningWeekTask { WeekId = currentWeek.Id, TaskId = tasks[1].Id }
            //    };
            //    context.CleaningWeekTasks.AddRange(weekTasks);
            //    context.SaveChanges();
            //}
        }
    }
}