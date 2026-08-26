using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Models
{
    public class Budget
    {
        public int Id { get; set; } // Unique identifier for the budget

        [Required(ErrorMessage = "Client name is required.")]
        public string Client {  get; set; } = string.Empty; // Name of the client


        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty; // Description of the budget


        [Range(1,int.MaxValue,ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; } // Quantity of items in the budget


        [Range(0.01, double.MaxValue,ErrorMessage ="The unit price must be a positive number.")] 
 
        public decimal UnitPrice { get; set; } // Unit price of the items in the budget

        public decimal TotalPrice => Quantity * UnitPrice; // Total price calculated from quantity and unit price


    }
}
