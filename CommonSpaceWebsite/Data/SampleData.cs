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




            if (!context.Users.Any())
            {

                // Add sample users
                var users = new User[]
                {
                new User { Name = "John", Password = "password123" },
                new User { Name = "Jane", Password = "letmein" },
                new User { Name = "Bob", Password = "securepw" }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }


            if (!context.CleaningTasks.Any())
            {
                // Add sample cleaning tasks
                var tasks = new CleaningTask[]
                {
                new CleaningTask { Name = "Vacuum hallway"},
                new CleaningTask { Name = "Clean kitchen" },
                new CleaningTask { Name = "Take out trash" }
                };
                context.CleaningTasks.AddRange(tasks);
                context.SaveChanges();
            }

            if (!context.ShoppingItems.Any()) { }
            var shopitems = new ShoppingItem[]
            {
                new ShoppingItem { Name = "Milk"},
                new ShoppingItem { Name = "Bread"},
                new ShoppingItem { Name = "Eggs"}
            };

            context.ShoppingItems.AddRange(shopitems);
            context.SaveChanges();
        }






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
