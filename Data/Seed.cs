using Microsoft.AspNetCore.Identity;
using InsaClub.Data.Enum;
using InsaClub.Models;
using Microsoft.EntityFrameworkCore;

namespace InsaClub.Data
{
    public class Seed
    {
        public static string mahdiMail = "mahdi@gmail.com";
        public static string mehdiMail = "mehdi@gmail.com";
        public static string houssemMail = "houssem@gmail.com";
        public static string azizMail = "aziz@gmail.com";

        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            string ACM = "INSAT ACM Student Chapter ";
            string CineClub = "Ciné-Radio INSAT";
            string INSATFootballClub = "INSAT football club";
            string IEEERAS = "IEEE RAS";
            string AEROBOTICS = "AeRobotix INSAT";
            string ANDROID = "Insat Android CLUB ";
            string VOLLLEYBALL = "VOLLLEYBALL";
            string BASKETBALL = "BASKETBALL";
            string CHESS = "Chess Enthusiasts INSAT";
            string IEECS = "IEEE CS";
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    var admin = context.Users.FirstOrDefaultAsync(u => u.UserName == "admin").Result;
                    var user = context.Users.FirstOrDefaultAsync(u => u.UserName == "user").Result;
                    var mahdi = context.Users.FirstOrDefaultAsync(u => u.Email == mahdiMail).Result;
                    var mehdi = context.Users.FirstOrDefaultAsync(u => u.Email == mehdiMail).Result;
                    var houssem = context.Users.FirstOrDefaultAsync(u => u.Email == houssemMail).Result;
                    var aziz = context.Users.FirstOrDefaultAsync(u => u.Email == azizMail).Result;

                    //Clubs



                    var ACMClub = new Club()
                    {
                        Title = ACM,
                        Image = "https://scontent.ftun10-2.fna.fbcdn.net/v/t39.30808-6/306732292_1020466055290532_8344076721484325768_n.jpg?_nc_cat=106&ccb=1-7&_nc_sid=efb6e6&_nc_ohc=3WvTkwxAKl4AX8TESrz&_nc_ht=scontent.ftun10-2.fna&oh=00_AfDgDpZ7Irhl2Q6qAwoflggeO2KlnL4_OROjmd8gOFW1bw&oe=65C954B8",
                        Description = "ACM is the abbreviation for Association for Computing Machinery. It is an international learned society for computing. It was founded in 1947, and is the world's largest scientific and educational computing society. The ACM is a non-profit professional membership group, with nearly 100,000 members as of 2019. Its headquarters are in New York City.",
                        ClubCategory = ClubCategory.SOFTWARE_ENGINEERING,
                        User = admin,
                        CreatedAt = new DateTime(2015, 1, 1),

                    };
                    ACMClub.Members = new List<MemberClub>()
                    {
                        new MemberClub()
                        {
                            User = admin,
                            Club = ACMClub,
                        },
                        new MemberClub()
                        {
                            User = mehdi,
                            Club = ACMClub,
                        },
                        new MemberClub()
                        {
                            User = houssem,
                            Club = ACMClub,
                        }
                    };

                    var CineClubClub = new Club()
                    {
                        Title = CineClub,
                        Image = "https://scontent.ftun10-2.fna.fbcdn.net/v/t39.30808-6/301905757_609508107278833_3814062779811054859_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=9c7eae&_nc_ohc=WRdoh0hyWdwAX8Kc9lI&_nc_oc=AQlnHRvcQctYA9xAuNR0y31G0UcYQAXbo4-cdNY0uFb4fg0ehl3kk_lR4G6Pmm7t_gQ&_nc_ht=scontent.ftun10-2.fna&oh=00_AfCkaH4aokG0gonnRCTgCIUixGJRgKqUKJ4xv7Ul1RwJyw&oe=65CA407C",
                        Description = "Cine Club is a club for movie lovers. We watch movies and discuss them. We also organize movie nights and movie marathons.",
                        ClubCategory = ClubCategory.CULTURAL,
                        User = user,
                        CreatedAt = new DateTime(2020, 12, 1),
                    };
                    CineClubClub.Members = new List<MemberClub>()
                    {

                        new MemberClub()
                        {
                            User = user,
                            Club = CineClubClub,
                        },
                        new MemberClub()
                        {
                            User = mehdi,
                            Club = CineClubClub,
                        }
                    };

                    var INSATFootbal = new Club()
                    {
                        Title = INSATFootballClub,
                        Image = "https://as1.ftcdn.net/v2/jpg/05/04/17/06/1000_F_504170662_GJIBhnhN4bw8PF5AzeHoAlgrH3adCLHO.jpg",
                        Description = "INSAT football club is a club for football lovers. We organize football matches and tournaments.",
                        ClubCategory = ClubCategory.SPORT,
                        User = user,
                    };
                    INSATFootbal.Members = new List<MemberClub>()
                    {
                        new MemberClub()
                        {
                            User = user,
                            Club = INSATFootbal,
                        },
                        new MemberClub()
                        {
                            User = mahdi,
                            Club = INSATFootbal,
                        }
                    };

                    var IEEERas = new Club()
                    {
                        Title = IEEERAS,
                        Image = "https://mesce.s3.ap-south-1.amazonaws.com/uploads/RAS_2_61974400bb.png",
                        Description = "IEEE Robotics and Automation Society (RAS) is a professional society of the IEEE that supports the development and the exchange of scientific knowledge in the fields of robotics and automation, including applied and theoretical issues.",
                        ClubCategory = ClubCategory.ROBOTICS,
                        User = admin,
                        CreatedAt = new DateTime(2015, 1, 2),
                    };
                    IEEERas.Members = new List<MemberClub>()
                    {
                        new MemberClub()
                        {
                            User = admin,
                            Club = IEEERas,
                        },
                        new MemberClub()
                        {
                            User = houssem,
                            Club = IEEERas,
                        }
                    };

                    var AeRobotix = new Club()
                    {
                        Title = AEROBOTICS,
                        Image = "https://scontent.ftun10-2.fna.fbcdn.net/v/t39.30808-6/241815259_1904289576399161_60547151752462615_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=efb6e6&_nc_ohc=ES3V1r2O2K0AX_XFW6B&_nc_ht=scontent.ftun10-2.fna&oh=00_AfC1N8RzXz8at_ojdiOgMCg_U308cFHTcGhNxtMbnbutmw&oe=65CB4479",
                        Description = "AeRobotix is a club for robotics lovers. We organize robotics competitions and workshops.",
                        ClubCategory = ClubCategory.ROBOTICS,
                        User = aziz,
                        CreatedAt = new DateTime(2015, 1, 3),
                    };
                    AeRobotix.Members = new List<MemberClub>()
                    {
                        new MemberClub()
                        {
                            User = aziz,
                            Club = AeRobotix,
                        },
                        new MemberClub()
                        {
                            User = mehdi,
                            Club = AeRobotix,
                        }
                    };

                    var Android = new Club()
                    {
                        Title = ANDROID,
                        Image = "https://scontent.ftun10-1.fna.fbcdn.net/v/t39.30808-6/402653827_388935590134493_6413141232685640507_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=efb6e6&_nc_ohc=sLJzQ2YIcLgAX_cLFVi&_nc_ht=scontent.ftun10-1.fna&oh=00_AfDP0utkfRh0zRb-fwOvrf2UrXie6gLVcAFvbgW81JVRqg&oe=65CB1BFF",
                        Description = "Insat Android Club is a club for android lovers. We organize android development workshops and competitions.",
                        ClubCategory = ClubCategory.SOFTWARE_ENGINEERING,
                        User = mehdi,
                        CreatedAt = new DateTime(2009, 1, 5),
                    };
                    Android.Members = new List<MemberClub>()
                    {
                        new MemberClub()
                        {
                            User = mehdi,
                            Club = Android,
                        },
                        new MemberClub()
                        {
                            User = mahdi,
                            Club = Android,
                        }
                    };

                    var volleyBall = new Club()
                    {
                        Title = VOLLLEYBALL,
                        Image = "https://img.freepik.com/vecteurs-libre/ballon-volley-ball_78370-346.jpg?size=338&ext=jpg&ga=GA1.1.1788068356.1706313600&semt=ais",
                        Description = "Volleyball is a club for volleyball lovers. We organize volleyball matches and tournaments.",
                        ClubCategory = ClubCategory.SPORT,
                        User = mahdi,
                        CreatedAt = new DateTime(2022, 5, 6),
                    };
                    volleyBall.Members = new List<MemberClub>()
                    {
                        new MemberClub()
                        {
                            User = mahdi,
                            Club = volleyBall,
                        },
                        new MemberClub()
                        {
                            User = houssem,
                            Club = volleyBall,
                        }
                    };

                    var basketball = new Club()
                    {
                        Title = BASKETBALL,
                        Image ="https://image.spreadshirtmedia.com/image-server/v1/products/T1459A839PA3861PT28D1021926013W9990H9990/views/1,width=378,height=378,appearanceId=839,backgroundColor=F2F2F2/basketball-logo-vector-sport.jpg",
                        Description = "Basketball is a club for basketball lovers. We organize basketball matches and tournaments.",
                        ClubCategory = ClubCategory.SPORT,
                        User = houssem,
                        CreatedAt = new DateTime(2022, 5, 7),
                    };
                    basketball.Members = new List<MemberClub>()
                    {
                        new MemberClub()
                        {
                            User = houssem,
                            Club = basketball,
                        },
                        new MemberClub()
                        {
                            User = mehdi,
                            Club = basketball,
                        }
                    };

                    var chess = new Club()
                    {
                        Title = CHESS,
                        Image = "https://scontent.ftun10-2.fna.fbcdn.net/v/t39.30808-6/241220700_104466978643234_3524018850468446300_n.png?_nc_cat=111&ccb=1-7&_nc_sid=efb6e6&_nc_ohc=WiZMy1Qfkd8AX-AwRgK&_nc_ht=scontent.ftun10-2.fna&oh=00_AfAuaXtyVL4jaLcoLeAeQ23uaekVQ5RiVsBbiCQqRoIMkQ&oe=65CB717D",
                        Description = "Chess is a club for chess lovers. We organize chess matches and tournaments.",
                        ClubCategory = ClubCategory.SPORT,
                        User = aziz,
                        CreatedAt = new DateTime(2019, 6, 8),
                    };
                    chess.Members = new List<MemberClub>()
                    {
                        new MemberClub()
                        {
                            User = aziz,
                            Club = chess,
                        },
                        new MemberClub()
                        {
                            User = mehdi,
                            Club = chess,
                        }
                    };

                    var IEECSClub = new Club()
                    {
                        Title = IEECS,
                        Image = "https://scontent.ftun10-2.fna.fbcdn.net/v/t39.30808-6/241815259_1904289576399161_60547151752462615_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=efb6e6&_nc_ohc=ES3V1r2O2K0AX_XFW6B&_nc_ht=scontent.ftun10-2.fna&oh=00_AfC1N8RzXz8at_ojdiOgMCg_U308cFHTcGhNxtMbnbutmw&oe=65CB4479",
                        Description = "IEECS is a club for computer science lovers. We organize computer science competitions and workshops.",
                        ClubCategory = ClubCategory.SOFTWARE_ENGINEERING,
                        User = mehdi,
                        CreatedAt = new DateTime(2013, 12, 3),
                    };
                    IEECSClub.Members = new List<MemberClub>()
                    {
                        new MemberClub()
                        {
                            User = mehdi,
                            Club = IEECSClub,
                        },
                        new MemberClub()
                        {
                            User = mahdi,
                            Club = IEECSClub,
                        }
                    };


                    var newCLubs = new List<Club>()
                    {
                        ACMClub,
                       CineClubClub,
                        INSATFootbal,
                        IEEERas,
                        AeRobotix,
                       Android,
                        volleyBall,
                       basketball,
                       chess,
                       IEECSClub
                    };




                    context.Clubs.AddRange(newCLubs);
                    context.SaveChanges();
                }
                //Events
                if (!context.Events.Any())
                {
                    var admin = context.Users.FirstOrDefaultAsync(u => u.UserName == "admin").Result;
                    var user = context.Users.FirstOrDefaultAsync(u => u.UserName == "user").Result;
                    var mahdi = context.Users.FirstOrDefaultAsync(u => u.Email == mahdiMail).Result;
                    var mehdi = context.Users.FirstOrDefaultAsync(u => u.Email == mehdiMail).Result;
                    var houssem = context.Users.FirstOrDefaultAsync(u => u.Email == houssemMail).Result;
                    var aziz = context.Users.FirstOrDefaultAsync(u => u.Email == azizMail).Result;

                    var ACMClub = context.Clubs.FirstOrDefault(c => c.Title == ACM);
                    var CineClubClub = context.Clubs.FirstOrDefault(c => c.Title == CineClub);
                    var INSATFootbal = context.Clubs.FirstOrDefault(c => c.Title == INSATFootballClub);
                    var IEERASClub = context.Clubs.FirstOrDefault(c => c.Title == IEEERAS);
                    var AeRobotix = context.Clubs.FirstOrDefault(c => c.Title == AEROBOTICS);
                    var Android = context.Clubs.FirstOrDefault(c => c.Title == ANDROID);
                    var VolleyBall = context.Clubs.FirstOrDefault(c => c.Title == VOLLLEYBALL);
                    var Basketball = context.Clubs.FirstOrDefault(c => c.Title == BASKETBALL);
                    var Chess = context.Clubs.FirstOrDefault(c => c.Title == CHESS);
                    var IEE = context.Clubs.FirstOrDefault(c => c.Title == IEECS);



                    var winterCupEvent = new Event()
                    {
                        Title = "Winter Cup",
                        Image = "https://wintercup.com/wp-content/uploads/wc2024.png",
                        Description = "This is the description of the first event",
                        EventCategory = EventCategory.COMPETITION,
                        Club = ACMClub

                    };
                    winterCupEvent.Members = new List<MemberEvent>()
                    {
                        new MemberEvent()
                        {
                            User = admin,
                            Event = winterCupEvent,
                        },
                        new MemberEvent()
                        {
                            User = mehdi,
                            Event = winterCupEvent,
                        }
                    };

                    var codeQuestEvent = new Event()
                    {
                        Title = "Code Quest",
                        Image = "https://scontent.ftun10-2.fna.fbcdn.net/v/t39.30808-6/402598779_6955319747862871_2681866326282106923_n.jpg?_nc_cat=108&ccb=1-7&_nc_sid=efb6e6&_nc_ohc=FyuxnDiVCjwAX9E5F9X&_nc_ht=scontent.ftun10-2.fna&oh=00_AfDfjq-_MBdLnRkUoKpQCpdoJEakbI8fa5wQ9Xv2dlvyYw&oe=65CA40C4",
                        Description = "This event is a coding competition. It is open to only beginners coders.",
                        EventCategory = EventCategory.COMPETITION,
                        Club = CineClubClub
                    };
                    codeQuestEvent.Members = new List<MemberEvent>()
                    {
                        new MemberEvent()
                        {
                            User = aziz,
                            Event = codeQuestEvent,
                        },
                        new MemberEvent()
                        {
                            User = mahdi,
                            Event = codeQuestEvent,
                        }
                    };

                    var nrwEvent = new Event()
                    {
                        Title = "National Robotics Weekend - NRW",
                        Image = "https://scontent.ftun10-2.fna.fbcdn.net/v/t39.30808-6/342357829_253617080387781_5339004576983973537_n.jpg?_nc_cat=109&ccb=1-7&_nc_sid=efb6e6&_nc_ohc=D9XqvUSwt8sAX_hoU5l&_nc_ht=scontent.ftun10-2.fna&oh=00_AfC8kZCS6ksi80OVccbp9cdB0RzXCSb1AkL5DotflLoJvg&oe=65CA6D70",
                        Description = "This event is a robotics competition. It is open to all robotics lovers.",
                        EventCategory = EventCategory.WORKSHOP,
                        Club = IEERASClub
                    };
                    nrwEvent.Members = new List<MemberEvent>()
                    {
                        new MemberEvent()
                        {
                            User = houssem,
                            Event = nrwEvent,
                        },
                        new MemberEvent()
                        {
                            User = admin,
                            Event = nrwEvent,
                        }
                    };

                    var aerodayEvent = new Event()
                    {
                        Title = "Aeroday",
                        Image = "https://scontent.ftun10-1.fna.fbcdn.net/v/t39.30808-6/327204484_1219366205330504_7035669247773453488_n.jpg?_nc_cat=105&ccb=1-7&_nc_sid=783fdb&_nc_ohc=pFjhuPVgpxYAX8tiG7Y&_nc_ht=scontent.ftun10-1.fna&oh=00_AfDH5YYs2ppGREeTT8Zoc0gnOQRmxRlW99JLTmzDAmQ__g&oe=65CAE141",
                        Description = "This event is a robotics competition. It is open to all robotics lovers.",
                        EventCategory = EventCategory.COMPETITION,
                        Club = AeRobotix
                    };
                    aerodayEvent.Members = new List<MemberEvent>()
                    {
                        new MemberEvent()
                        {
                            User = mehdi,
                            Event = aerodayEvent,
                        },
                        new MemberEvent()
                        {
                            User = aziz,
                            Event = aerodayEvent,
                        }
                    };

                    var androidDayEvent = new Event()
                    {
                        Title = "Android Day",
                        Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d7/Android_robot.svg/1200px-Android_robot.svg.png",
                        Description = "This event is a workshop for android development. It is open to all android lovers.",
                        EventCategory = EventCategory.WORKSHOP,
                        Club = Android
                    };
                    androidDayEvent.Members = new List<MemberEvent>()
                    {
                        new MemberEvent()
                        {
                            User = mehdi,
                            Event = androidDayEvent,
                        },
                        new MemberEvent()
                        {
                            User = mahdi,
                            Event = androidDayEvent,
                        }
                    };

                    var insatGotTalent = new Event()
                    {
                        Title = "INSAT Got Talent",
                        Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/08/Got_Talent_logo.PNG/800px-Got_Talent_logo.PNG",
                        Description = "This event is a talent show. It is open to all talented people.",
                        EventCategory = EventCategory.COMPETITION,
                        Club = CineClubClub
                    };
                    insatGotTalent.Members = new List<MemberEvent>()
                    {
                        new MemberEvent()
                        {
                            User = houssem,
                            Event = insatGotTalent,
                        },
                        new MemberEvent()
                        {
                            User = aziz,
                            Event = insatGotTalent,
                        }
                    };


                    var footballTournament = new Event()
                    {
                        Title = "Football Tournament",
                        Image = "https://static.vecteezy.com/ti/vecteur-libre/p3/5696170-football-vecteur-sport-logo-vecteur-vectoriel.jpg",
                        Description = "This event is a football tournament. It is open to all football lovers.",
                        EventCategory = EventCategory.COMPETITION,
                        Club = INSATFootbal

                    };

                    footballTournament.Members = new List<MemberEvent>()
                    {
                        new MemberEvent()
                        {
                            User = mahdi,
                            Event = footballTournament,
                        },
                        new MemberEvent()
                        {
                            User = mehdi,
                            Event = footballTournament,
                        }
                    };


                    context.Events.AddRange(new List<Event>()
                    {
                        winterCupEvent,
                        codeQuestEvent,
                        nrwEvent,
                        aerodayEvent,
                        androidDayEvent,
                        insatGotTalent,
                        footballTournament
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                {
                    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

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
                    Console.WriteLine("*******************Users and roles have been seeded successfully.*************");
                    if (adminUser == null)
                    {

                        var newAdminUser = new User()
                        {
                            UserName = "admin",
                            Email = adminUserEmail,
                            EmailConfirmed = true,
                            FirstName = "Admin",
                            LastName = "Admin",
                            StudyLevelId = getStudyLevelId(context, new StudyLevel()
                            {
                                Level = EStudyLevel.A1,
                                Speciality = ESpeciality.MPI
                            }).Result,
                            Bio = "I am the admin of this website. I am responsible for managing the website and the users."

                        };
                        Console.WriteLine($"*********************Creating user: {newAdminUser.UserName}****************************");
                        await userManager.CreateAsync(newAdminUser, "Admin2$*");
                        Console.WriteLine($"Created user: {newAdminUser.UserName} ({newAdminUser.Id})");

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
                            FirstName = "User",
                            LastName = "User",
                            StudyLevelId = getStudyLevelId(context, new StudyLevel()
                            {
                                Level = EStudyLevel.A3,
                                Speciality = ESpeciality.CH
                            }).Result,
                            Bio = "I am a user of this website. I am responsible for managing the website and the users."

                        };
                        Console.WriteLine($"*********************Creating user: {newUser.UserName}****************************");
                        await userManager.CreateAsync(newUser, "User2$");
                        Console.WriteLine($"Created user: {newUser.UserName} ({newUser.Id})");
                        await userManager.AddToRoleAsync(newUser, UserRoles.User);
                    }


                    var MahdiUser = new User()
                    {
                        UserName = "Mahdi",
                        Email = mahdiMail,
                        EmailConfirmed = true,
                        FirstName = "Mahdi",
                        LastName = "Chaabane",
                        StudyLevelId = getStudyLevelId(context, new StudyLevel()
                        {
                            Level = EStudyLevel.A3,
                            Speciality = ESpeciality.RT

                        }).Result,
                        Bio = "I am Mahdi Chaabane, I love to code and I am a member of the ACM club.",
                        ProfileImageUrl = "https://scontent.ftun10-2.fna.fbcdn.net/v/t39.30808-6/277667770_5532793263414790_2623260051412729718_n.jpg?_nc_cat=103&ccb=1-7&_nc_sid=efb6e6&_nc_ohc=MCNcX-DK8gUAX-cFpO1&_nc_ht=scontent.ftun10-2.fna&oh=00_AfDnIsduduV1Y1QkVdb1XihPGEvTIcEVAYQpBqeTVi2rZA&oe=65CA8D51"

                    };
                    await userManager.CreateAsync(MahdiUser, "Mahdi2$");
                    await userManager.AddToRoleAsync(MahdiUser, UserRoles.User);

                    var MehdiUser = new User()
                    {
                        UserName = "Mehdi",
                        Email = mehdiMail,
                        EmailConfirmed = true,
                        FirstName = "Mehdi",
                        LastName = "Fkih",
                        StudyLevelId = getStudyLevelId(context, new StudyLevel()
                        {
                            Level = EStudyLevel.A4,
                            Speciality = ESpeciality.IIA

                        }).Result,
                        Bio = "I am Mehdi Fkih, I love playing football and watching movies.",
                        ProfileImageUrl = "https://scontent.ftun10-2.fna.fbcdn.net/v/t39.30808-6/315859035_3487022184913711_7223019709064075587_n.jpg?_nc_cat=108&ccb=1-7&_nc_sid=dd5e9f&_nc_ohc=Hr-9yUKPYGQAX_jI5ii&_nc_ht=scontent.ftun10-2.fna&oh=00_AfD4WhHjnqY6NhVhJJ6X_saGxhsALMHk3vtL1DUeNfQh5Q&oe=65CA010E"

                    };
                    await userManager.CreateAsync(MehdiUser, "Mehdi2$");
                    await userManager.AddToRoleAsync(MehdiUser, UserRoles.User);

                    var HoussemUser = new User()
                    {
                        UserName = "Houssem",
                        Email = houssemMail,
                        EmailConfirmed = true,
                        FirstName = "Houssem",
                        LastName = "Sahnoun",
                        StudyLevelId = getStudyLevelId(context, new StudyLevel()
                        {
                            Level = EStudyLevel.A3,
                            Speciality = ESpeciality.CH

                        }).Result,
                        Bio = "I don't write bios.",
                        ProfileImageUrl = "https://scontent.ftun10-1.fna.fbcdn.net/v/t31.18172-8/14608699_2133697200189036_3070639874557288745_o.jpg?_nc_cat=104&ccb=1-7&_nc_sid=be3454&_nc_ohc=Epe7eEFs8gwAX_pf0_G&_nc_oc=AQl2ICK3hdD6ZxWaG9GdIwxAXCow1Yu4WEFEN3QeRW2hj-A_ALm40CiISrVM5dY5WJM&_nc_ht=scontent.ftun10-1.fna&oh=00_AfCoiEYE4Lk00ZkGJncA2VxK3668y8zWqDvS3hlk53UDig&oe=65ED657F",


                    };
                    await userManager.CreateAsync(HoussemUser, " Houssem2$");
                    await userManager.AddToRoleAsync(HoussemUser, UserRoles.User);

                    var AzizUser = new User()
                    {
                        UserName = "Aziz",
                        Email = azizMail,
                        EmailConfirmed = true,
                        FirstName = "Aziz",
                        LastName = "Masmoudi",
                        StudyLevelId = getStudyLevelId(context, new StudyLevel()
                        {
                            Level = EStudyLevel.A5,
                            Speciality = ESpeciality.IMI

                        }).Result,
                        Bio = "I am Aziz Masmoudi, I am a member of the IEEE RAS club.",
                        ProfileImageUrl = "https://scontent.ftun10-1.fna.fbcdn.net/v/t39.30808-6/316168593_3572163966392214_3545248607243677642_n.jpg?_nc_cat=100&ccb=1-7&_nc_sid=efb6e6&_nc_ohc=i9EhoH-lkYAAX_EzBl9&_nc_ht=scontent.ftun10-1.fna&oh=00_AfDQvapuGKorjf4gVfqv8CMEQtDazpaA5aEiHjxFXWhokw&oe=65CB72F7"

                    };
                    await userManager.CreateAsync(AzizUser, "Aziz2$");
                    await userManager.AddToRoleAsync(AzizUser, UserRoles.User);


                }
            }
        }
        public static async Task<int> getStudyLevelId(ApplicationDbContext context, StudyLevel studyLevel)
        {



            // Check if StudyLevel exists, otherwise create it
            var existingStudyLevel = await context.StudyLevels
                .FirstOrDefaultAsync(s => s.Level == studyLevel.Level && s.Speciality == studyLevel.Speciality);

            if (existingStudyLevel == null)
            {
                // StudyLevel does not exist, create it
                context.StudyLevels.Add(studyLevel);
                await context.SaveChangesAsync(); // Save changes to get the generated StudyLevelId
                existingStudyLevel = studyLevel;
            }
            return existingStudyLevel.Id;
        }



    }
}
