using Microsoft.AspNetCore.Identity;
using Sales_System.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {


        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {

            if (!userManager.Users.Any()) {

                var user = new AppUser() {
                
                DisplayName= "Abdullah Al-Najjar",
                Email = "abdullahalnajjar@gmail.com",
                UserName="abdullahalnajjar",
                PhoneNumber="01009435746",
                };
                await userManager.CreateAsync(user,"P@ssw0rd");    
            }



        }
    }
}
