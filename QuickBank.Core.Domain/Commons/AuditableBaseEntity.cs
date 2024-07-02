namespace QuickBank.Core.Domain.Commons
{
    public class AuditableBaseEntity
    {
        public virtual int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
    }
}
