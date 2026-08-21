using BudgetApp.Data;
using BudgetApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers;

public class BudgetController : Controller
{
    private readonly BudgetContext _context;


    public BudgetController(BudgetContext context)
    {
        _context = context;
    }
     public IActionResult Index()
    {
        var budgets = _context.Budgets.ToList();
        return View(budgets);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    } 

    [HttpPost]
    public IActionResult Create(Budget budget)
    {
        _context.Budgets.Add(budget);
        _context.SaveChanges();
        return RedirectToAction("Index");

        //return View();
    }
}