using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechSupport.Data.Common.Models;

namespace TechSupport.Data.Models
{
    public class CustomerCard : AuditInfo
    {
        private ICollection<User> customers;

        public CustomerCard()
        {
            this.customers = new HashSet<User>();
        }

        public CustomerCard(string password, string userId)
            : this()
        {
            this.CustomerCardPassword = password;
            this.UserId = userId;
        }

        [Key]
        public string Id { get; set; }

        public int? CategoryId { get; set; }

        public virtual CustomerCardCategory Category { get; set; }

        [Required]
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
        public string Phone { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public string Summary { get; set; }

        public decimal Price { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Informed { get; set; }

        public bool Warranty { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        /// <remarks>
        /// If ContestPassword is null the contest can be competed by everyone without require a password.
        /// If the ContestPassword is not null the contest participant should provide a valid password.
        /// </remarks>
        [MaxLength(20)]
        public string CustomerCardPassword { get; set; }

        [NotMapped]
        public bool HasCustomerCardPassword => this.CustomerCardPassword != null;

        public virtual ICollection<User> Customers
        {
            get { return this.customers; }
            set { this.customers = value; }
        }
    }
}