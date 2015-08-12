using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechSupport.Data.Common.Models;

namespace TechSupport.Data.Models
{
    public class CustomerCardCategory : DeletableEntity
    {
        private ICollection<CustomerCardCategory> children;

        private ICollection<CustomerCard> customerCards;

        public CustomerCardCategory()
        {
            this.children = new HashSet<CustomerCardCategory>();
            this.customerCards = new HashSet<CustomerCard>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }

        public virtual CustomerCardCategory Parent { get; set; }

        [InverseProperty("Parent")]
        public virtual ICollection<CustomerCardCategory> Children
        {
            get { return this.children; }
            set { this.children = value; }
        }

        public virtual ICollection<CustomerCard> CustomerCards
        {
            get { return this.customerCards; }
            set { this.customerCards = value; }
        }

        public bool IsVisible { get; set; }
    }
}