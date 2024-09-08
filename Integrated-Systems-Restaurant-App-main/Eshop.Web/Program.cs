
using ERestaurant.Repository;
using ERestaurant.Repository.Implementation;
using ERestaurant.Repository.Interface;
using ERestaurant.Service;
using ERestaurant.Service.Implementation;
using ERestaurant.Service.Interface;
using EShop.Domain;
using EShop.Domain.Identity;
using EShop.Service.Implementation;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Restaurant.Domain.Domain;
using Stripe.Climate;
using System.Reflection.Emit;

var builder = WebApplication.CreateBuilder(args);



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<CostumerUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddTransient<IRestaurantService, RestaurantService>();
builder.Services.AddTransient<IMenuService, MenuService>();
builder.Services.AddTransient<IMenuItemService, MenuItemService>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<EShop.Service.Interface.IOrderService, EShop.Service.Implementation.OrderService>();
builder.Services.AddScoped<ERestaurant.Service.Interface.IDeliveryPersonService, ERestaurant.Service.Implementation.DeliveryPersonService>();
builder.Services.AddScoped<AlbumService>();

builder.Services.AddHttpClient<AlbumService>();

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



var app = builder.Build();

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

app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
