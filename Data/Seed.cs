using Microsoft.AspNetCore.Identity;
using InsaClub.Data.Enum;
using InsaClub.Models;
using Microsoft.EntityFrameworkCore;

namespace InsaClub.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    var admin = context.Users.FirstOrDefaultAsync(u => u.UserName == "admin").Result;
                    var user = context.Users.FirstOrDefaultAsync(u => u.UserName == "user").Result;

                    context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Title = "ACM",
                            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8e/Association_for_Computing_Machinery_%28ACM%29_logo.svg/1200px-Association_for_Computing_Machinery_%28ACM%29_logo.svg.png",
                            Description = "ACM is the abbreviation for Association for Computing Machinery. It is an international learned society for computing. It was founded in 1947, and is the world's largest scientific and educational computing society. The ACM is a non-profit professional membership group, with nearly 100,000 members as of 2019. Its headquarters are in New York City.",
                            ClubCategory = ClubCategory.SOFTWARE_ENGINEERING,
                            User = admin
                         },
                        new Club()
                        {
                            Title = "Cine Club",
                            Image = "https://static.wixstatic.com/media/8d8500_6b9d7d43e7654b6c9d5c6bc5c7ff1105~mv2.png",
                            Description = "Cine Club is a club for movie lovers. We watch movies and discuss them. We also organize movie nights and movie marathons.",
                            ClubCategory = ClubCategory.CULTURAL,
                            User = user
                          
                        },
                        new Club()
                        {
                            Title = "INSAT football club",
                            Image = "https://as1.ftcdn.net/v2/jpg/05/04/17/06/1000_F_504170662_GJIBhnhN4bw8PF5AzeHoAlgrH3adCLHO.jpg",
                            Description = "INSAT football club is a club for football lovers. We organize football matches and tournaments.",
                            ClubCategory = ClubCategory.SPORT,
                            User = user
                       
                        },
                        new Club()
                        {
                            Title = "IEEE RAS",
                            Image = "https://mesce.s3.ap-south-1.amazonaws.com/uploads/RAS_2_61974400bb.png",
                            Description = "This is the description of the first club",
                            ClubCategory = ClubCategory.ROBOTICS,
                            User = admin
                           
                        }
                    });
                    context.SaveChanges();
                }
                //Events
                if (!context.Events.Any())
                {
                    var ACM = context.Clubs.FirstOrDefault(c => c.Title == "ACM");
                    var IEE = context.Clubs.FirstOrDefault(c => c.Title == "IEEE RAS");
                    context.Events.AddRange(new List<Event>()
                    {
                        new Event()
                        {
                            Title = "Winter Cup",
                            Image="https://wintercup.com/wp-content/uploads/wc2024.png",
                            Description = "This is the description of the first event",
                            EventCategory = EventCategory.COMPETITION,
                            Club = ACM
                          
                        },
                        new Event()
                        {
                            Title = "NRW",
                            Image="https://media.licdn.com/dms/image/C5603AQHyTv3fwTpP7g/profile-displayphoto-shrink_800_800/0/1576673144493?e=2147483647&v=beta&t=l4v0SGLhbAfv0Eq2IdHlYIHDitaoDelxfEXf4DKcPAs",
                            Description = "This is the description of the first event",
                            EventCategory = EventCategory.HACKATHON,
                            Club = IEE
                          
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdminUser, "admin");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string UserEmail = "user@gmail.com";

                var User = await userManager.FindByEmailAsync(UserEmail);
                if (User == null)
                {
                    var newUser = new User()
                    {
                        UserName = "user",
                        Email = UserEmail,
                        EmailConfirmed = true,
                    
                    };
                    await userManager.CreateAsync(newUser, "user");
                    await userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
            }
        }
    }
}