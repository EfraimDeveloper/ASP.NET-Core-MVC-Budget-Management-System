using BudgetApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BudgetApp.Controllers;
using BudgetApp.Models;
namespace BudgetControllerTests
{
    public class ControllerTests
    {
        [Fact]
        public void Index_ShouldReturnView()
        {

            var options = new DbContextOptionsBuilder<BudgetContext>()
                .UseInMemoryDatabase("BudgetTest")
                .Options;

            using var context = new BudgetContext(options);

            var buget = new Budget
            {
                Client = "João",
                Description = "Computador",
                Quantity = 2,
                UnitPrice = 500
            };
            context.Budgets.Add(buget);
            context.SaveChanges();

            var controller=new BudgetController(context);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var budgets=Assert.IsType <List<Budget>>(viewResult.Model);

            Assert.Single(budgets);

            Assert.Equal("João", budgets[0].Client);
            Assert.Equal("Computador", budgets[0].Description);
            Assert.Equal(2, budgets[0].Quantity);
            Assert.Equal(500, budgets[0].UnitPrice);
            Assert.Equal(1000, budgets[0].TotalPrice);


        }
    }
}