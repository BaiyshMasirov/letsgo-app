namespace Domain.Common
{
    public class AuditableEntity : BaseEntity
    {
        /// <summary>Created by (user id)</summary>
        public Guid CreatedBy { get; set; }

        /// <summary>Created date</summary>
        public DateTime Created { get; set; }

        /// <summary>Modified by (user id)</summary>
        public Guid? ModifiedBy { get; set; }

        /// <summary>Modified date</summary>
        public DateTime? Modified { get; set; }
    }
}