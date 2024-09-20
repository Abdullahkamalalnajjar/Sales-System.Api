using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sales_System.Core.Entities.Identity;
using Sales_System.Core.Services;
using Sales_System.Repository.Identity;
using Sales_System.Service;
using System.Text;

namespace Sales_System.Api.Extentions
{
    public static class IdentityServicesExtention
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection Services ,IConfiguration configuration)
        {

            Services.AddScoped(typeof(ITokenService), typeof(TokenService));


            Services.AddIdentity<AppUser, IdentityRole>(option => { 
            
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireUppercase = true;
                option.Password.RequireNonAlphanumeric = true;
            
            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            Services.AddAuthentication(option => { 
                option.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            
            })
                     .AddJwtBearer(option => {
                         // ده ال هيعمل ماتش بين token  JWTو اعداته نفس ال ف 

                         option.TokenValidationParameters=new TokenValidationParameters()
                         {
                             ValidateIssuer = true,
                             ValidIssuer =configuration["JWT:ValidIssuer"],
                             ValidateAudience = true,
                             ValidAudience=configuration["JWT:ValidAudience"],
                             ValidateLifetime= true,
                             ValidateIssuerSigningKey = true,
                             IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                     };


                     });

            return Services;

        }
    }
}
