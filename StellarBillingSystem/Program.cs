using StellarBillingSystem.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security;
using StellarBillingSystem.Business;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

/*string encryptedConnectionString = builder.Configuration.GetConnectionString("BillingDBConnection");
string decryptedConnectionString = EncryptionHelper.Decrypt(encryptedConnectionString);

builder.Services.AddDbContext<BillingContext>(options =>
    options.UseSqlServer(decryptedConnectionString));*/


builder.Services.AddDbContext<BillingContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BillingDBConnection")));



builder.Services.AddDbContext<BillingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/LoginAuthentication/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    }) ;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseSession();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LoginAuthentication}/{action=Login}/{id?}");

app.Run();
