using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using BankAdministration.Data;
using BankAdministration.Services.StatisticsService.CustomerStatistics;
using BankAdministration.Services.StatisticsService.EconomyStatistics;
using BankAdministration.Services.StatisticsService.UserStatistics;
using BankAdministration.Services.CustomerService.GeoLocation;
using BankAdministration.Services.CustomerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BankContext>();
builder.Services.AddTransient<IEconomyStatisticsService, EconomyStatisticsService>();
builder.Services.AddTransient<ICustomerStatisticsService, CustomerStatisticsService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<IRealAccountService, RealAccountService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<IUserStatisticsService, UserStatisticsService>();
builder.Services.AddTransient<DataInitializer>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DataInitializer>().SeedData();
}

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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
