using System;

namespace WebApplication1.Models
{
    public class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }

    public class MustHaveTenant<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
