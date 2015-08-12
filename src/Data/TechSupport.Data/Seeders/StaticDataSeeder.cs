using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using TechSupport.Common;
using TechSupport.Data.Models;

namespace TechSupport.Data.Seeders
{
    internal static class StaticDataSeeder
    {
        private static readonly Random random = new Random();

        internal static void SeedUsers(TechSupportDbContext context)
        {
            var names = GetUserNames();

            var userManager = new UserManager<User>(new UserStore<User>(context));

            for (int i = 0; i < names.Length; i++)
            {
                var user = new User()
                {
                    UserName = string.Format("FakeUser{0}@FakeEmail.com", i + 1),
                    Email = string.Format("FakeUser{0}@FakeEmail.com", i + 1),
                    Address = string.Format("Karlovo{0}", i + 1),
                    FirstName = names[i].Substring(0, names[i].IndexOf(" ")),
                    LastName = names[i].Substring(names[i].IndexOf(" ") + 1)
                };

                userManager.Create(user, "qwerty");

                userManager.AddToRole(user.Id, GlobalConstants.DefaultRole);

                context.SaveChanges();
            }
        }

        internal static void SeedAdmin(TechSupportDbContext context)
        {
            const string AdminEmail = "qwe@qwe.com";
            const string AdminPassword = "qweqwe";

            if (context.Users.Any(u => u.Email == AdminEmail))
            {
                return;
            }

            var userManager = new UserManager<User>(new UserStore<User>(context));

            var admin = new User
            {
                FirstName = "Pesho",
                LastName = "Admina",
                Email = AdminEmail,
                Address = "Sopot",
                UserName = AdminEmail
            };

            userManager.Create(admin, AdminPassword);
            userManager.AddToRole(admin.Id, GlobalConstants.AdminRole);
            userManager.AddToRole(admin.Id, GlobalConstants.ModeratorRole);
            userManager.AddToRole(admin.Id, GlobalConstants.DefaultRole);

            context.SaveChanges();
        }

        internal static void SeedModerator(TechSupportDbContext context)
        {
            const string moderatorEmail = "moderator@moderator.com";
            const string mderatorPassword = "moderator123456";

            if (context.Users.Any(u => u.Email == moderatorEmail))
            {
                return;
            }

            var userManager = new UserManager<User>(new UserStore<User>(context));

            var admin = new User
            {
                FirstName = "Gosho",
                LastName = "Moderatora",
                Email = moderatorEmail,
                Address = "Sopot",
                UserName = moderatorEmail
            };

            userManager.Create(admin, mderatorPassword);

            userManager.AddToRole(admin.Id, GlobalConstants.ModeratorRole);
            userManager.AddToRole(admin.Id, GlobalConstants.DefaultRole);

            context.SaveChanges();
        }

        internal static void SeedRoles(TechSupportDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            roleManager.Create(new IdentityRole { Name = GlobalConstants.DefaultRole });
            roleManager.Create(new IdentityRole { Name = GlobalConstants.AdminRole });
            roleManager.Create(new IdentityRole { Name = GlobalConstants.ModeratorRole });

            context.SaveChanges();
        }

        private static string[] GetUserNames()
        {
            return new[]
            {
                "Polly Dimitrova",
                "Petur Toshev",
                "Aleksii Todorov",
                "Dimitur Stoyanov",
                "Anna Georgieva",
                "Viktor Ivanov",
                "Vicktoria Petrova",
                "Sara Merkenzel",
                "Gosho Petrov",
                "Pesho Georgiev",
                "Ivan Klisurski",
                "Matt Deamon",
                "Peter Stoyanov",
                "Dragomir Petrov",
                "Dimitur Trifonov"
            };
        }

        private static DateTime GetDate()
        {
            var date = DateTime.Now;
            date.AddDays((-1) * random.Next(0, 30));
            date.AddHours((-1) * random.Next(0, 23));
            date.AddMinutes((-1) * random.Next(0, 60));
            return date;
        }

        public static void SeedCustomerCard(TechSupportDbContext context)
        {
            if (context.CustomerCards.Any())
            {
                return;
            }
            AddData(context);
        }

        private static void AddData(TechSupportDbContext context)
        {
            var names = GetCustomerName();

            var customer = new Customer()
            {
                IsOfficial = true,
                CreatedOn = GetDate(),
                IsDeleted = false,
                IsHidden = false
            };
            context.Customers.Add(customer);
            context.SaveChanges();
            var data = new CustomerCard();

            for (int i = 0; i < names.Length; i++)
            {
                data.CustomerFirstName = names[i].Substring(0, names[i].IndexOf(" "));
                data.CustomerLastName = names[i].Substring(names[i].IndexOf(" ") + 1);
                data.CustomerAddress = string.Format("Karlovo{0}", i + 1);
                //data.Description = cc[i];
                data.Price = i + 10;
                data.IsVisible = true;
                //data.CustomerCardPassword = "123";
                data.Informed = false;
                //data.Comment = cc[i];
                //data.EnrollmentDate = DateTime.Now;
                //data.EndDate = DateTime.Now;
                data.Warranty = true;
                data.CustomerId = customer.Id;
                data.CreatedOn = GetDate();
                data.IsHidden = false;
                data.IsDeleted = false;
                context.CustomerCards.Add(data);
                context.SaveChanges();
            }
        }

        private static string[] GetCustomerName()
        {
            return new[]
            {
                "Polly Dimitrova",
                "Petur Toshev",
                "Aleksii Todorov",
                "Dimitur Stoyanov",
                "Anna Georgieva",
                "Viktor Ivanov",
                "Vicktoria Petrova"
            };
        }
    }
}