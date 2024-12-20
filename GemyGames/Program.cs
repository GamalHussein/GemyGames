

namespace GemyGames
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(optios =>
            optios.UseSqlServer(builder.Configuration.GetConnectionString("DefultConection")));
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
                    
            builder.Services.AddScoped<ICategoreServices,CategoreService>();
            builder.Services.AddScoped<IDiviceServices, DiviceServices>();
            builder.Services.AddScoped<IGamesService, GamesService>();
















            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Games}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
