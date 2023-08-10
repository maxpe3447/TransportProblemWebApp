using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TransportProblemWebApp.Domain;
using TransportProblemWebApp.Domain.Repositories.InformationFieldRepository;
using TransportProblemWebApp.Domain.Repositories.TextFieldRepository;
using TransportProblemWebApp.Service;
using TransportProblemWebApp.Service.MinElementAlgorithm;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Bind("Project", new Config());
builder.Configuration.Bind("AdminArea", new AdminAreaConfig());

builder.Services.AddTransient<ITextFieldRepository, TextFieldRepository>();
builder.Services.AddTransient<IInformationFieldRepository, InformationFieldRepository>();
builder.Services.AddTransient<DataManager>();
builder.Services.AddTransient<ITransportAlgorithm, TransportAlgorithm>();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
{
    option.User.RequireUniqueEmail = true;
    option.Password.RequiredLength = 10;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireDigit = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Auth";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
});
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminArea", policy => policy.RequireRole("admin"));
});
builder.Services.AddControllersWithViews(x =>
{
    x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
});
var app = builder.Build();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
        