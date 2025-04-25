namespace CommonSpaceWebsite.Models
{
    public class CleaningTask
    {
        public int Id { get; set; }                     // Unique task ID
        public string Name { get; set; }                // Short name ("Vacuum hallway")
        public string Description { get; set; }         // Detailed instructions
                  
 
        public List<CleaningWeekTask> WeekTasks { get; set; } = new List<CleaningWeekTask>();
    }
}
