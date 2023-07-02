using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemo.Model.View;
using RazorPageDemo.Data;
using RazorPageDemo.Model;// Replace with your database context namespace

namespace YourProjectNamespace.Pages.Account
{
    public class ListModel : PageModel
    {
        private readonly RDbContext _dbContext; // Replace with your database context class

        public List<Register> registers { get; set; }

        public ListModel(RDbContext dbContext) // Inject the database context
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            registers = _dbContext.registers.ToList(); // Fetch users from the database
        }
    }
}
