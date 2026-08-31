using BudgetApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BudgetApp.Controllers;
using BudgetApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

            var controller = new BudgetController(context);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var budgets = Assert.IsType<List<Budget>>(viewResult.Model);

            Assert.Single(budgets);

            Assert.Equal("João", budgets[0].Client);
            Assert.Equal("Computador", budgets[0].Description);
            Assert.Equal(2, budgets[0].Quantity);
            Assert.Equal(500, budgets[0].UnitPrice);
            Assert.Equal(1000, budgets[0].TotalPrice);


        }
        [Fact]
        public void Create_get_ShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<BudgetContext>()
                .UseInMemoryDatabase("CreateGetTest")
                .Options;

            using var context = new BudgetContext(options);

            var controller = new BudgetController(context);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_post_ShouldSavelBuggetAndRedirect()
        {
            var options = new DbContextOptionsBuilder<BudgetContext>()
            .UseInMemoryDatabase("createPostTest")
            .Options;

            using var context = new BudgetContext(options);

            var controller = new BudgetController(context);

            var budget = new Budget
            {
                Client = "Paul",
                Description = "Software",
                Quantity = 3,
                UnitPrice = 200
            };


            var result = controller.Create(budget);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectResult.ActionName);

            var savedBuget = context.Budgets.Single();

            Assert.Equal("Paul", savedBuget.Client);
            Assert.Equal("Software", savedBuget.Description);
            Assert.Equal(3, savedBuget.Quantity);
            Assert.Equal(200, savedBuget.UnitPrice);

        }
        [Fact]
        public void Create_Post_InvalidModel_ShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<BudgetContext>()
                .UseInMemoryDatabase("createInvalidTest")
                .Options;

            using var context = new BudgetContext(options);

            var controller = new BudgetController(context);

            controller.ModelState.AddModelError("Client", "Erro");

            var budget = new Budget
            {
                Client = "Paul",
                Description = "Software",
                Quantity = 3,
                UnitPrice = 200
            };

            var result = controller.Create(budget);

            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void Edit_ShouldReturnView_WhenBudgetExists()
        {
            var optins = new DbContextOptionsBuilder<BudgetContext>()
                .UseInMemoryDatabase("editTest")
                .Options;

            using var context = new BudgetContext(optins);

            var budget = new Budget
            {
                Client = "Carlos",
                Description = "Computador",
                Quantity = 2,
                UnitPrice = 500
            };

            context.Budgets.Add(budget);
            context.SaveChanges();

            var controller = new BudgetController(context);

            var result = controller.Edit(budget.Id);

            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public void Edit_ShouldReturnNotFound_WhenBudgetDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<BudgetContext>()
                .UseInMemoryDatabase("editNotFoundTest")
                .Options;

            using var context = new BudgetContext(options);

            var controller = new BudgetController(context);

            var result = controller.Edit(999); 

            Assert.IsType<NotFoundResult>(result);
         }
        [Fact]  
        public void Edit_post_shouldUpdateBudgetAndRedirect()
        {
            var option = new DbContextOptionsBuilder<BudgetContext>()
                .UseInMemoryDatabase("editPostTest")
                .Options;

            using var context=new BudgetContext(option);


            var budget = new Budget
            {
                Client = "Carlos",
                Description = "Computador",
                Quantity = 2,
                UnitPrice = 500
            };

            context.Budgets.Add(budget);
            context.SaveChanges();

            var controller=new BudgetController(context);

            budget.Client = "Carlo update";
            budget.UnitPrice= 600;

            var result = controller.Edit(budget);

            var redirectResult=Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

          
        }


        [Fact]
        public void Edit_Post_InvalidModel_ShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<BudgetContext>()
                .UseInMemoryDatabase("editInvalidTest")
                .Options;

            using var context = new BudgetContext(options);

            var controller = new BudgetController(context);

            controller.ModelState.AddModelError("Client", "Erro");

            var budget = new Budget
            {
                Client = "Carlos",
                Description = "Computador",
                Quantity = 2,
                UnitPrice = 500
            };

            var result = controller.Edit(budget);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnView_WhenBudgetExists()
        {
            var options = new DbContextOptionsBuilder<BudgetContext>()
                .UseInMemoryDatabase("deleteTest")
                .Options;

            using var context = new BudgetContext(options);

            var budget = new Budget
            {
                Client = "Ana",
                Description = "Website",
                Quantity = 1,
                UnitPrice = 800
            };

            context.Budgets.Add(budget);
            context.SaveChanges();

            var controller = new BudgetController(context);

            var result = controller.Delete(budget.Id);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnNotFound_WhenBudgetDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<BudgetContext>()
                .UseInMemoryDatabase("deleteNotFoundTest")
                .Options;

            using var context = new BudgetContext(options);

            var controller = new BudgetController(context);

            var result = controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteConfirmed_ShouldDeleteBudgetAndRedirect()
        {
            var options = new DbContextOptionsBuilder<BudgetContext>()
                .UseInMemoryDatabase("deleteConfirmedTest")
                .Options;

            using var context = new BudgetContext(options);

            var budget = new Budget
            {
                Client = "Ana",
                Description = "Website",
                Quantity = 1,
                UnitPrice = 800
            };

            context.Budgets.Add(budget);
            context.SaveChanges();

            var controller = new BudgetController(context);

            var result = controller.DeleteConfirmed(budget.Id);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectResult.ActionName);

            Assert.Empty(context.Budgets);
        }

        [Fact]
        public void DeleteConfirmed_ShouldReturnNotFound_WhenBudgetDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<BudgetContext>()
                .UseInMemoryDatabase("deleteConfirmedNotFoundTest")
                .Options;

            using var context = new BudgetContext(options);

            var controller = new BudgetController(context);

            var result = controller.DeleteConfirmed(999);

            Assert.IsType<NotFoundResult>(result);
        }

    } 
}