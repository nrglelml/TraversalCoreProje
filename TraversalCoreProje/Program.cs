using BusinessLayer.Container;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using TraversalCoreProje.Models;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        path: Path.Combine(Directory.GetCurrentDirectory(), "Logs", "Log1.txt"),
        rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.CustomerValidator();
builder.Services.AddControllersWithViews().AddFluentValidation();
builder.Services.AddValidatorsFromAssemblyContaining<AnnouncementValidator>();
builder.Services.AddDbContext<Context>();

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<Context>()
    .AddErrorDescriber<CustomIdentityValidator>();

builder.Services.AddHttpClient();
builder.Services.ContainerDependencies();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddAuthorization(options =>
{
    // Admin area'sý için sadece Admin rolü
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));

    // Member area'sý için giriþ yapmýþ herhangi bir kullanýcý
    options.AddPolicy("MemberPolicy", policy =>
        policy.RequireAuthenticatedUser());
});

builder.Services.AddMvc(config =>
{
    // Global olarak giriþ zorunluluðu
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/SignIn";
    options.LogoutPath = "/Login/LogOut";
    options.AccessDeniedPath = "/ErrorPage/AccessDenied"; 
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();