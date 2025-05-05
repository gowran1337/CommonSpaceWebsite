using CommonSpaceWebsite.Data;
using CommonSpaceWebsite.Migrations;
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


        [BindProperty]
        public string InputTaskName { get; set; }

        [BindProperty]
        public string ShoppingItemName { get; set; }

        [BindProperty]
        public string InputUserName { get; set; }

        [BindProperty]
        public string InputUserPassword { get; set; }




        public List<ShoppingItem> ShoppingItems { get; set; } = new List<ShoppingItem>(); 
		public List<CleaningWeek> CleaningWeeks { get; set; } = new List<CleaningWeek>();
        public List<CleaningTask> CleaningTasks { get; set; } = new List<CleaningTask>();
        public List<User> Users { get; set; } = new List<User>();
        


        public async Task OnGetAsync() //fetch data from the data base and put them in the lists above
        {
            CleaningTasks = await _context.CleaningTasks.ToListAsync();
            Users = await _context.Users.ToListAsync();
            ShoppingItems = await _context.ShoppingItems.ToListAsync(); //gets shopping items from the database

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

        public async Task<IActionResult> OnPostLoginUser()
        {
            if (string.IsNullOrWhiteSpace(InputUserName) || string.IsNullOrWhiteSpace(InputUserPassword))
            {
                ModelState.AddModelError("UserName", "Username and password cannot be empty");
                return RedirectToPage();
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == InputUserName && u.Password == InputUserPassword);
            HttpContext.Session.SetString("username", InputUserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "Invalid username or password");
                return RedirectToPage();
            }
            // User is authenticated, redirect to a different page or perform other actions
            return RedirectToPage(); // Example: redirect to a dashboard page
        }

        public async Task<IActionResult> OnPostRegisterUser()
        {
            if (string.IsNullOrWhiteSpace(InputUserName) || string.IsNullOrWhiteSpace(InputUserPassword))
            {
                ModelState.AddModelError("UserName", "Username and password cannot be empty");
                return RedirectToPage();
            }
            var user = new User
            {
                Name = InputUserName,
                Password = InputUserPassword,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostAddShoppingItem() //method to add a task to the database        
            
        {
            Console.WriteLine("abdi"); // Debugging line to check the value of ShoppingItemName
            if (string.IsNullOrWhiteSpace(ShoppingItemName))
            {
                ModelState.AddModelError("ShoppingitemName", " name cannot be empty");
                return RedirectToPage();
            }

            var item = new ShoppingItem
                {
                   Name = ShoppingItemName,                
               };
            
            _context.ShoppingItems.Add(item); 
            await _context.SaveChangesAsync();
            
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddTask() //method to add a task to the database
        {
            if (string.IsNullOrWhiteSpace(InputTaskName))
            {
                ModelState.AddModelError("TaskName", "Task name cannot be empty");
                return RedirectToPage();
            }

            var task = new CleaningTask
            {
                Name = InputTaskName,
            };

            _context.CleaningTasks.Add(task);
            await _context.SaveChangesAsync();
          


            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostDeleteItem(int id, string itemType)
        {

             if (itemType == "shoppingItem")
            {
                var item = await _context.ShoppingItems.FindAsync(id);
                if (item != null)
                {
                    _context.ShoppingItems.Remove(item);
                }
            }
      
            else if (itemType == "task")
            {
                var task = await _context.CleaningTasks.FindAsync(id);
                if (task != null)
                {
                    _context.CleaningTasks.Remove(task);
                    
                }                
            }
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }






        public List<CleaningWeek> AssignWeeksToUsersLoop(List<User> users, DateTime startDate, DateTime endDate) //method to to setup cleaning order
        {
            var cleaningWeeks = new List<CleaningWeek>();

            if(users==null || users.Count == 0)
            {
                return cleaningWeeks; // Return an empty list if no users are available
            }

            int userIndex = 0; // value equal to the index of the user to assign week to
            DateTime currentStartDate = startDate; // sets the start date to the current date, getting the current date from the parameter on the get method

            while (currentStartDate < endDate) //loop runs until the end date is reached
            {
                var week = new CleaningWeek
                {
                    StartDate = currentStartDate, 
                    EndDate = currentStartDate.AddDays(6), // Assuming a week is 7 days
                    CleanerId = users[userIndex].Id // Assign the user ID
                };

                cleaningWeeks.Add(week);
                userIndex++; // after a week has been created and a user assigned, the index is increased by 1 to assign the next user

                if (userIndex >= users.Count)  // resets the count if the last user has been reached
                {
                    userIndex = 0; 
                }


                currentStartDate = currentStartDate.AddDays(7); // Move to the next week

            }
                return cleaningWeeks;
        }
        

    }
}
