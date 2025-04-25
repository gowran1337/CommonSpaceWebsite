namespace CommonSpaceWebsite.Models
{
    public class CleaningWeekTask
    {
        public int Id { get; set; }
        public int WeekId { get; set; }                // Which week this belongs to
        public int TaskId { get; set; }                 // Which standard task
        public bool IsCompleted { get; set; }           // Done or not?
        public DateTime? CompletedDate { get; set; }    // When it was completed

        public CleaningWeek Week { get; set; }          // Link to the week
        public CleaningTask Task { get; set; }          // Link to task details
    }
}
