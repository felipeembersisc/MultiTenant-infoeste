namespace CursoInfoeste.Models.Base;

public abstract class BaseTenantEntity: BaseEntity
{
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }
}