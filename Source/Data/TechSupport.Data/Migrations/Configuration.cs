namespace TechSupport.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TechSupport.Data.Seeders;

    public sealed class Configuration : DbMigrationsConfiguration<TechSupportDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TechSupportDbContext context)
        {
            if (!context.Users.Any())
            {
                StaticDataSeeder.SeedRoles(context);
                StaticDataSeeder.SeedAdmin(context);
                StaticDataSeeder.SeedModerator(context);
                StaticDataSeeder.SeedUsers(context);
            }
            // StaticDataSeeder.SeedServiceCard(context);

            StaticDataSeeder.SeedCustomerCard(context);
        }
    }
}