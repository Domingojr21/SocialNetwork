

namespace RedSocial.Core.Domain.Common
{
    public abstract class CommonProperties
    {
        public int Id { get; set; } 
        public string UserId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

    }
}
