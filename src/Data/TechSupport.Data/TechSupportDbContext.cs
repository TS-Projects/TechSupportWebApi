using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using TechSupport.Data.Common;
using TechSupport.Data.Migrations;
using TechSupport.Data.Models;

namespace TechSupport.Data
{
    public class TechSupportDbContext : IdentityDbContext<User>, ITechSupportDbContext
    {
        public TechSupportDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TechSupportDbContext, Configuration>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<TechSupportDbContext>());
        }

        public static TechSupportDbContext Create()
        {
            return new TechSupportDbContext();
        }

        public virtual IDbSet<CustomerCard> CustomerCards { get; set; }

        public virtual IDbSet<Customer> Customers { get; set; }

        public virtual IDbSet<CustomerCardCategory> CustomerCardCategories { get; set; }

        public virtual IDbSet<CustomerCardQuestion> CustomerCardQuestions { get; set; }

        public virtual IDbSet<CustomerCardQuestionAnswer> CustomerCardQuestionAnswers { get; set; }

        public virtual IDbSet<CustomerAnswer> CustomerAnswers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder); // Without this call EntityFramework won't be able to configure the identity model
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            //this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        //private void ApplyDeletableEntityRules()
        //{
        //    // Approach via @julielerman: http://bit.ly/123661P
        //    foreach (
        //        var entry in
        //            this.ChangeTracker.Entries()
        //                .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
        //    {
        //        var entity = (IDeletableEntity)entry.Entity;

        //        entity.DeletedOn = DateTime.Now;
        //        entity.IsDeleted = true;
        //        entry.State = EntityState.Modified;
        //    }
        //}

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }
    }
}