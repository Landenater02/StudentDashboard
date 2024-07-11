using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentDashboard.Data;
using StudentDashboard.Controllers;
namespace StudentDashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<StudentDashboardContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDashboardContext") ?? throw new InvalidOperationException("Connection string 'StudentDashboardContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

                        builder.Services.AddEndpointsApiExplorer();

                        builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

                        if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

                        app.MapVisitsEndpoints();

                        app.MapstudentEndpoints();

            app.Run();
        }
    }
}
