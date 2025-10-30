using CursoInfoeste.Services;

namespace CursoInfoeste;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;
    
    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, Persistencia persistencia)
    {
        var tenantClaim = context.User?.Claims?.FirstOrDefault(x => x.Type == "TenantId");

        if (tenantClaim != null && !string.IsNullOrEmpty(tenantClaim.Value))
        {
            persistencia.TenantId = int.Parse(tenantClaim.Value);
        }
        
        Console.WriteLine("Antes de próximo middleware");
        
        await _next(context);
        
        Console.WriteLine("Depois de próximo middleware");
    }
}