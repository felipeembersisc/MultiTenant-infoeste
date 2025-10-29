using CursoInfoeste.Models.Base;

namespace CursoInfoeste.Models;

public class Product: BaseTenantEntity
{
    public string Name { get; set; }
    public decimal Value { get; set; }
}