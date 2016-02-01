using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechSupport.Data.Common.Models;

namespace TechSupport.Data.Models
{
    public class CustomerAnswer : AuditInfo
    {
        [Key, Column(Order = 1)]
        public int CustomerId { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }

        [Key, Column(Order = 2)]
        public int CustomerCardQuestionId { get; set; }

        [Required]
        public virtual CustomerCardQuestion CustomerCardQuestion { get; set; }

        public string Answer { get; set; }
    }
}