using E_Learning.BL.ServicesExtention;
using E_Learning.DAL.Data.Context;
using E_Learning.DAL.ServicesExtension;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            /*------------------------------------------------------------------------*/
            builder.Services.AddBLServices();
            builder.Services.AddDALServices(builder.Configuration);
            /*------------------------------------------------------------------------*/
            var app = builder.Build();
            /*------------------------------------------------------------------------*/
            //using (var scope = app.Services.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //    var roles = new[] { "Admin", "User" };
            //    foreach (var roleName in roles)
            //    {
            //        if (!await roleManager.RoleExistsAsync(roleName))
            //        {
            //            await roleManager.CreateAsync(new IdentityRole(roleName));
            //        }
            //    }
            //}
            /*------------------------------------------------------------------------*/

            //// Another way to update database automatically after migration
            //using (var scope = app.Services.CreateScope())
            //{
            //    var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            //    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

            //    try
            //    {
            //        await _context.Database.MigrateAsync();
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = loggerFactory.CreateLogger<Program>();
            //        logger.LogError(ex, "Error during migration");
            //    }
            //}

            /*------------------------------------------------------------------------*/

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
