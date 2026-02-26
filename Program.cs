using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using MyMvcApp.Services;

var builder = WebApplication.CreateBuilder(args);
// Add database services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//declare the services here...
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISportService, SportService>();


// We are adding a "Global Filter" to every single controller in the app
builder.Services.AddControllersWithViews(options =>
{
    // This stops the browser from taking "photographs" of ANY page!
    // Now the Back/Forward buttons will NEVER show a cached secure page.
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.ResponseCacheAttribute 
    { 
        NoStore = true, 
        Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.None 
    });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();
app.Run();
