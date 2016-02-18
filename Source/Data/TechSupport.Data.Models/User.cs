namespace TechSupport.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TechSupport.Data.Common;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TechSupport.Data.Common.Models;


    public class User : IdentityUser, IDeletableEntity
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

        //[Required]
        //[MaxLength(80)]
        //[EmailAddress]
        //[Index(IsUnique = true)]
        //public string Email { get; set; }

        //[Required]
        //[StringLength(ValidationConstants.MaxUserUserNameLength)]
        //[Index(IsUnique = true)]
        //public string UserName { get; set; }

        //public bool IsAdmin { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string About { get; set; }

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