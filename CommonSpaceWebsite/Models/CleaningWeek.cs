namespace CommonSpaceWebsite.Models
{
    public class CleaningWeek
    {
        public int Id { get; set; }                     // Unique week ID
        public DateTime StartDate { get; set; }          // When the rotation starts
        public DateTime EndDate { get; set; }           // When it ends
        public int CleanerId { get; set; }              // Who's responsible

        // Navigation to related data:
        public User Cleaner { get; set; }               // The resident object
        public List<CleaningWeekTask> Tasks { get; set; } = new List<CleaningWeekTask>();
        public List<ShoppingItem> ShoppingList { get; set; } = new List<ShoppingItem>();
    }
}
