namespace CommonSpaceWebsite.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Password { get; set; }

        public List<CleaningWeek> AssignedWeeks { get; set; } = new List<CleaningWeek>();
        public List<ShoppingItem> PurchasedItems { get; set; } = new List<ShoppingItem>();

    }
}
