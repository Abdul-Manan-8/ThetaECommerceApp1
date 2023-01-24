using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThetaECommerceApp.Data;
using ThetaECommerceApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//cls8
//create variable for our connection string
var MyDBCSVar = builder.Configuration.GetConnectionString("MyDBCS");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//cls8
//add data base name and connectionstring of our own
builder.Services.AddDbContext<theta_ecommerce_dbContext>(options =>
    options.UseSqlServer(MyDBCSVar));



builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o => {

    o.IdleTimeout = TimeSpan.FromMinutes(20);
});



//builder.Services.Add


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
