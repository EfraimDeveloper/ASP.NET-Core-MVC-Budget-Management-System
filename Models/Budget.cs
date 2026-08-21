namespace BudgetApp.Models
{
    public class Budget
    {
        public int Id { get; set; } // Unique identifier for the budget

        public string Client {  get; set; } = string.Empty; // Name of the client

        public string Description { get; set; } = string.Empty; // Description of the budget

        public int quantity { get; set; } // Quantity of items in the budget

        public decimal UnitPrice { get; set; } // Unit price of the items in the budget

        public decimal TotalPrice => quantity * UnitPrice; // Total price calculated from quantity and unit price


    }
}
