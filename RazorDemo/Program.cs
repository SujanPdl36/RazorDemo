using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RazorPageDemo.Data;
using RazorPageDemo.Model;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Configure Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<RDbContext>()
    .AddDefaultTokenProviders(); // Add this line to add default token providers
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "YourCookieName";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = "/Users/Login";
    options.AccessDeniedPath = "/Users/AccessDenied";
    options.SlidingExpiration = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Seed roles and users
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    SeedRoles(roleManager);
    SeedUsers(userManager);
}

app.Run();

void SeedRoles(RoleManager<IdentityRole> roleManager)
{
    var roles = new[] { "SuperAdmin", "CommonUser" };

    foreach (var role in roles)
    {
        if (!roleManager.RoleExistsAsync(role).Result)
        {
            var identityRole = new IdentityRole { Name = role };
            roleManager.CreateAsync(identityRole).Wait();
        }
    }
}

void SeedUsers(UserManager<IdentityUser> userManager)
{
    var superAdminEmail = "admin@example.com";
    var superAdminPassword = "SuperAdminPassword123!";

    if (userManager.FindByEmailAsync(superAdminEmail).Result == null)
    {
        var superAdmin = new IdentityUser
        {
            UserName = superAdminEmail,
            Email = superAdminEmail
        };

        var result = userManager.CreateAsync(superAdmin, superAdminPassword).Result;

        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(superAdmin, "SuperAdmin").Wait();
        }
    }
}
