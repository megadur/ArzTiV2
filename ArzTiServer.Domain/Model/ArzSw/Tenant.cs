using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArzTi3Server.Domain.Model.ArzSw
{
    [Table("tenant")]
    public class Tenant
    {
        [Key]
        [Column("tenant_id")]
        public string TenantId { get; set; }
        
        [Column("tenant_name")]
        public string TenantName { get; set; }
        
        [Column("connection_string")]
        public string ConnectionString { get; set; }
        
        [Column("is_active")]
        public bool IsActive { get; set; } = true;
        
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        [Column("description")]
        public string Description { get; set; }
    }
}