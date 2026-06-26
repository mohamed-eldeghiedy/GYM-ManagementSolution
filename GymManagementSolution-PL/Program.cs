using Microsoft.Extensions.Options;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using DAL.Repositories.Interfaces;
using DAL.Repositories.Classes;
using DAL.Data.DataSeed;
using BLL;
using BLL.Interfaces;
using BLL.Classes;

namespace GymManagementSolution_PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GymDbConnection")));
            //builder.Services.AddScoped(typeof(IRepository<>) , typeof(GenericRepository<>)); 
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<IMemberService , MemberService>();
            builder.Services.AddScoped<IAnalyticsService , AnalyticsService>();
            builder.Services.AddAutoMapper(x=>x.AddProfile(new MappingProfiles()));


            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())dbContext.Database.Migrate();
            GymDbContextSeed.SeedData(dbContext);


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
