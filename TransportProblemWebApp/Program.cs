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

            var app = builder.Build();
            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("defaulte", "{controller=Home}/{action=Index}/{id?}");
            });
            

            app.Run();
        }
    }
}