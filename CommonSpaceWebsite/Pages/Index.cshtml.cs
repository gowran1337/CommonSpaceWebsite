using CommonSpaceWebsite.Data;
using CommonSpaceWebsite.Models;// only runs method "AssignWeeksToUsersLoop" if there are no weeks in the database
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CommonSpaceWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CommonSpaceDbContext _context;
        public IndexModel(CommonSpaceDbContext context)
        {
            _context = context;
        }

        public List<CleaningWeek> CleaningWeeks { get; set; } = new List<CleaningWeek>();
        public List<CleaningTask> CleaningTasks { get; set; } = new List<CleaningTask>();
        public List<User> Users { get; set; } = new List<User>();


        public async Task OnGetAsync() //fetch data from the data base and put them in the lists above
        {
            CleaningTasks = await _context.CleaningTasks.ToListAsync();
            Users = await _context.Users.ToListAsync();

            CleaningWeeks = await _context.CleaningWeeks //gets cleaningweeks
               .Include(week => week.Cleaner)  //inside the cleaningweek object we also want to get the cleaner object to be able to display the name of the cleaner
               .ToListAsync();



            if (CleaningWeeks == null || CleaningWeeks.Count == 0 || CleaningWeeks.Max(w => w.EndDate) < DateTime.Today)
            // only runs the method "AssignWeeksToUsersLoop" if there are no weeks in the database
            //OR if the last week in the database is older than today meaning 3 months have passed and we need new weeks
            {
                var newWeeks = AssignWeeksToUsersLoop(Users, DateTime.Today, DateTime.Today.AddMonths(3));

                _context.CleaningWeeks.AddRange(newWeeks);
                await _context.SaveChangesAsync();

                CleaningWeeks = newWeeks;

            }
        }



        public List<CleaningWeek> AssignWeeksToUsersLoop(List<User> users, DateTime startDate, DateTime endDate) //method to to setup cleaning order
        {
            var cleaningWeeks = new List<CleaningWeek>();

            if(users==null || users.Count == 0)
            {
                return cleaningWeeks; // Return an empty list if no users are available
            }

            int userIndex = 0; // value equal to the index of the user to assign week to
            DateTime currentStartDate = startDate;

            while (currentStartDate < endDate) //loop runs until the end date is reached
            {
                var week = new CleaningWeek
                {
                    StartDate = currentStartDate,
                    EndDate = currentStartDate.AddDays(6), // Assuming a week is 7 days
                    CleanerId = users[userIndex].Id // Assign the user ID
                };

                cleaningWeeks.Add(week);
                userIndex++;

                if (userIndex >= users.Count)
                {
                    userIndex = 0; // Reset to the first user
                }

                currentStartDate = currentStartDate.AddDays(7); // Move to the next week

            }
                return cleaningWeeks;
        }
        

    }
}
