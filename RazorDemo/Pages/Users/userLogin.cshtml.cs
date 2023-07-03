using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorDemo.Model.View;
using RazorPageDemo.Data;
using RazorPageDemo.Model;
using System.Threading.Tasks;


namespace RazorDemo.Pages.Users
{
    public class userLoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RDbContext _dbContext;

        [BindProperty]
        public newLogin Input { get; set; }

        public userLoginModel(SignInManager<IdentityUser> signInManager, RDbContext dbContext)
        {
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _dbContext.registers.FirstOrDefaultAsync(u => u.Email == Input.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, Input.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToPage("/Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return Page();
        }
    }
}
