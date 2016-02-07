using TechSupport.Data.Common;

namespace TechSupport.Data.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser
    {
        public User()
        {
            // This will prevent UserManager.CreateAsync from causing exception
            this.CreatedOn = DateTime.Now;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        public bool IsAdmin { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        public string LastName { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        public string City { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        public string Address { get; set; }

        // IAuditInfo
        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsHidden { get; set; }

        // IDeletableEntity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}