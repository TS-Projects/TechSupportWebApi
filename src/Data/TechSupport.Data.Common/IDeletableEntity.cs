namespace TechSupport.Data.Common
{
    using System;

    public interface IDeletableEntity : IAuditInfo
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}