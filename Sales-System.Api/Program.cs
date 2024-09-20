using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sales_System.Api.Configurations.Cors;
using Sales_System.Api.Extentions;
using Sales_System.Core.Entities.Identity;
using Sales_System.Core.Services;
using Sales_System.Repository.AppDbContext;
using Sales_System.Repository.Identity;
using Sales_System.Service;

namespace Sales_System.Api
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

            #region ConnectionDatabase
            //-----Identity---------
            builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            //-----Application---------

            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            });
            #endregion

            #region Extention Services 
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddApplicationServices();  
            #endregion


            var app = builder.Build();


            #region Update-Database
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var ILoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                //--------IdentityDbcontext-----------
                var IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();
                var userManger = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(userManger);
                //-----ApplicationDbcontext-----------

                var dbContext = services.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {

                var logger = ILoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "ِan error Occured during apply the Migration");
            }

            #endregion

            // Configure the HTTP request pipeline.
           // if (app.Environment.IsDevelopment())
           // {
                app.UseSwagger();
                app.UseSwaggerUI();
           // }

            app.UseHttpsRedirection();
            app.UseCorsSetup();

            // تأكد من إضافة هذا السطر لتفعيل المصادقة
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
