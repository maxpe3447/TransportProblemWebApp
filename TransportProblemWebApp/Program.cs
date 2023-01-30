using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TransportProblemWebApp.Domain;
using TransportProblemWebApp.Domain.Repositories.InformationFieldRepository;
using TransportProblemWebApp.Domain.Repositories.TextFieldRepository;
using TransportProblemWebApp.Service;

namespace TransportProblemWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Configuration.Bind("Project", new Config());
            builder.Configuration.Bind("AdminArea", new AdminAreaConfig());

            builder.Services.AddTransient<ITextFieldRepository, TextFieldRepository>();
            builder.Services.AddTransient<IInformationFieldRepository, InformationFieldRepository>();
            builder.Services.AddTransient<DataManager>();

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

            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("defaulte", "{controller=Home}/{action=Index}/{id?}");
            });
            

            app.Run();
        }
    }
}