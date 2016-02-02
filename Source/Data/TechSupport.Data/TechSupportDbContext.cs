using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechSupport.Data.Common;
using TechSupport.Data.Common.Models;
using TechSupport.Data.Migrations;
using TechSupport.Data.Models;

namespace TechSupport.Data
{
    public class TechSupportDbContext : IdentityDbContext<User>
    {
        public TechSupportDbContext()
            : base("TechSupport")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TechSupportDbContext, Configuration>());
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

        public static TechSupportDbContext Create()
        {
            return new TechSupportDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditInfoRules()
        {
            var changedAudits = this.ChangeTracker.Entries()
                    .Where(e => e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in changedAudits)
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
    }
}