namespace CommonSpaceWebsite.Models
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public string Name { get; set; }                // Item name ("Dish soap")
        public bool? IsPurchased { get; set; }          // Bought or not?
        public int? PurchasedById { get; set; }         // Who bought it

        public User? PurchasedBy { get; set; }           // Link to buyer
        public CleaningWeek? Week { get; set; }   // Reference to the week
    }
}
