using Microsoft.EntityFrameworkCore;
using DIERENTUIN13.Data;
using DIERENTUIN13.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddControllers(); // Add this line to include API controllers

// Configure the database context
builder.Services.AddDbContext<DIERENTUIN13Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DIERENTUIN13Context")));

// Add the DataSeeder as a service
builder.Services.AddTransient<DataSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapControllers(); // Add this line to map API controllers

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DIERENTUIN13Context>();
    var seeder = services.GetRequiredService<DataSeeder>();
    context.Database.Migrate();
    seeder.Seed();
}

app.Run();
