using CommonSpaceWebsite.Data;
using CommonSpaceWebsite.Models;
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


        public List<CleaningTask> CleaningTasks { get; set; } = new List<CleaningTask>();
        public List<User> Users { get; set; } = new List<User>();


        public async Task OnGetAsync() //fetch data from the data base and put them in the lists above
        {
            CleaningTasks = await _context.CleaningTasks.ToListAsync();
            Users = await _context.Users.ToListAsync();
        }

      



    }
}
