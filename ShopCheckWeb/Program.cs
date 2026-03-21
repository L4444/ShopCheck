using Microsoft.EntityFrameworkCore;
using ShopCheckDb;
using ShopCheckWeb.Mocks;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShopCheckDbContext>(options => options.UseSqlite("Data Source=Shopping.db"));
builder.Services.AddScoped<IShopCheckService, ShopCheckService>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


using(var scope = app.Services.CreateScope())
{
    var con = scope.ServiceProvider.GetRequiredService<ShopCheckDbContext>();
    con.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
