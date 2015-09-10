using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechSupport.Data.Common.Models;

namespace TechSupport.Data.Models
{
    public class CustomerCardQuestion : AuditInfo
    {
        private ICollection<CustomerCardQuestionAnswer> answers;
        private ICollection<CustomerAnswer> customerAnswers;

        public CustomerCardQuestion()
        {
            this.answers = new HashSet<CustomerCardQuestionAnswer>();
            this.customerAnswers = new HashSet<CustomerAnswer>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("CustomerCard")]
        public int CustomerCardId { get; set; }

        public virtual CustomerCard CustomerCard { get; set; }

        public string Text { get; set; }

        [DefaultValue(true)]
        public bool AskOfficialCustomers { get; set; }

        [DefaultValue(true)]
        public bool AskPracticeCustomers { get; set; }

        //public ContestQuestionType Type { get; set; }

        public virtual ICollection<CustomerCardQuestionAnswer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }

        public virtual ICollection<CustomerAnswer> CustomerAnswers
        {
            get { return this.customerAnswers; }
            set { this.customerAnswers = value; }
        }

        public string RegularExpressionValidation { get; set; }
    }
}