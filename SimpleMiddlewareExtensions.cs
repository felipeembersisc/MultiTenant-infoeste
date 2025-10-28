namespace CursoInfoeste;

public static class SimpleMiddlewareExtensions
{
    public static IApplicationBuilder UseSimpleMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TenantMiddleware>();
    }
}