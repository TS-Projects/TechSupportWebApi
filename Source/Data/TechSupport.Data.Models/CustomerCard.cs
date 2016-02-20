using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechSupport.Data.Common.Models;

namespace TechSupport.Data.Models
{
    public class CustomerCard : AuditInfo
    {
        private ICollection<CustomerCardQuestion> questions;

        public CustomerCard()
        {
            this.questions = new HashSet<CustomerCardQuestion>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsVisible { get; set; }

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

        [MaxLength(20)]
        public string CustomerCardPassword { get; set; }

        public int CustomerId { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<CustomerCardQuestion> Questions
        {
            get { return this.questions; }
            set { this.questions = value; }
        }

        [NotMapped]
        public bool HasCustomerCardPassword
        {
            get
            {
                return this.CustomerCardPassword != null;
            }
        }
    }
}