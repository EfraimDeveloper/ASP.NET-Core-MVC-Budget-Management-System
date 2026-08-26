using BudgetApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Tests
{
   
    public class BudgetTests
    {
        [Fact]
        public void TotalPrice_shouldCalcuteCorrectly()
        {
            var budget = new Budget
            {
                Quantity = 2,
                UnitPrice = 10
            };

            Assert.Equal(20,budget.TotalPrice);
        }

        [Fact]
        public void Quantity_shouldBePositive()
        {
            var budget = new Budget
            {
                Quantity = 0,
                UnitPrice = 10
            };
            var context=new ValidationContext(budget);
            var results=new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(budget, context, results,true);

            Assert.False(isvalid);
        }

        [Fact]
        public void Unitprice_shouldBePositive()
        {
            var budget = new Budget
            {
                Quantity = 2,
                UnitPrice = 0,
            };

            var context = new ValidationContext(budget);

            var results=new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(budget, context, results, true);

            Assert.False(isvalid);
        }

        [Fact]
        public void Client_ShoudBeRequired()
        {
            var budget = new Budget
            {
                Client = ""
            };

            var context = new ValidationContext(budget);
            var results = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(budget, context, results, true);

            Assert.False(isvalid);

        }
        [Fact]
        public void Description_shouldBeRequired()
        {
            var budget=new Budget
            {
                Description = ""
            };
            var context = new ValidationContext(budget);
            var results = new List<ValidationResult>();
            var isvalid = Validator.TryValidateObject(budget, context, results, true);
            Assert.False(isvalid);
        }

        [Fact]
        public void Quantity_ShouldBeNegative()
        {
            var budget = new Budget
            {
                Quantity = -1,
                UnitPrice = 10
            };

            var context=new ValidationContext(budget);
            var results=new List<ValidationResult>();
            var isvalid=Validator.TryValidateObject(budget, context,results, true);

            Assert.False(isvalid);
        }
        [Fact]
        public void UniPrice_ShouldBeNegative()
        {
            var budget = new Budget
            {
                Quantity = 2,
                UnitPrice= -10
            };

            var context=new ValidationContext(budget);
            var results=new List<ValidationResult>();
            var isValid=Validator.TryValidateObject(budget, context,results,true);

            Assert.False(isValid);


        }
    }
}
