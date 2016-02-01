using System.ComponentModel.DataAnnotations;
using TechSupport.Data.Common.Models;

namespace TechSupport.Data.Models
{
    public class CustomerCardQuestionAnswer : AuditInfo
    {
        [Key]
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public virtual CustomerCardQuestion Question { get; set; }

        public string Text { get; set; }
    }
}