using CursoInfoeste.Services;

namespace CursoInfoeste;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Persistencia _persistencia;
    
    public TenantMiddleware(RequestDelegate next, Persistencia persistencia)
    {
        _next = next;
        _persistencia = persistencia;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var tenantId = context.Request.Headers["TenantId"].ToString();
        _persistencia.TenantId = int.Parse(tenantId);
        
        Console.WriteLine("Antes de próximo middleware");
        
        await _next(context);
        
        Console.WriteLine("Depois de próximo middleware");
    }
}