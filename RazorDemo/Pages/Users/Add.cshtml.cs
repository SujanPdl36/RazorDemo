using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorDemo.Model.View;
using RazorPageDemo.Data;
using RazorPageDemo.Model;

namespace RazorDemo.Pages.Users
{
    public class AddModel : PageModel
    {
        private RDbContext dbcontext;
        private readonly UserManager<ApplicationUser> userManager;
        public AddModel(RDbContext dbContext)
        {
            this.dbcontext = dbContext;


        }
        [BindProperty]
        public NewRegister newRegister { get; set; }

        public string Role { get; set; }

        public SelectList RoleList { get; set; } = new SelectList(new[] { "Super Admin", "Common User" });

        public void OnGet()
        {
            ViewData["Roles"] = RoleList;
        }

        public void OnPost()
        {
            var NewRegister = new Register
            {
                FullName = newRegister.FullName,
                Address = newRegister.Address,
                Email = newRegister.Email,
                Password = newRegister.Password,
            };
            dbcontext.registers.Add(NewRegister);
            dbcontext.SaveChanges();

        }
        public void OnPostUpdate()
        {
           

            var existingUser = dbcontext.registers.FirstOrDefault(u => u.Id == newRegister.Id);

            if (existingUser != null)
            {
                existingUser.FullName = newRegister.FullName;
                existingUser.Address = newRegister.Address;
                existingUser.Email = newRegister.Email;
                existingUser.Password = newRegister.Password;

                dbcontext.SaveChanges();
            }
        }
        public IActionResult OnPostDelete()
        {
            var user = userManager.GetUserAsync(User).Result;
            if (user != null && user.Role == "Super Admin")
            {
                var existingUser = dbcontext.registers.FirstOrDefault(u => u.Id == newRegister.Id);

                if (existingUser != null)
                {
                    dbcontext.registers.Remove(existingUser);
                    dbcontext.SaveChanges();
                }
            }

            return RedirectToPage("Index");
        }

    }
}