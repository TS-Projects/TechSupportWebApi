using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechSupport.Data.Common.Models;

namespace TechSupport.Data.Models
{
    public class Customer : AuditInfo
    {
        private ICollection<CustomerCard> customerCards;
        private ICollection<CustomerAnswer> answers;

        public Customer()
        {
            this.customerCards = new HashSet<CustomerCard>();
            this.answers = new HashSet<CustomerAnswer>();
        }

        public Customer(int customerCardId, string userId, bool isOfficial)
            : this()
        {
            this.Id = customerCardId;
            this.UserId = userId;
            this.IsOfficial = isOfficial;
        }

        [Key]
        public int Id { get; set; }

        public bool IsOfficial { get; set; }

        public virtual ICollection<CustomerCard> CustomerCards
        {
            get { return this.customerCards; }
            set { this.customerCards = value; }
        }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<CustomerAnswer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }
    }
}