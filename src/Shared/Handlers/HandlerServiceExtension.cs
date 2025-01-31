using System.Reflection;

namespace CompanyRatingApi.Shared.Handlers;

public static class HandlerServiceExtension
{
    public static void AddHandlersFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        // Get all types that implement IHandler<,> interface
        var handlerTypes = assembly.GetTypes()
            .Where(x => x.GetInterfaces()
                .Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandler<,>)))
            .ToList();

        // Register all handler types
        foreach (var handlerType in handlerTypes)
        {
            services.AddScoped(handlerType);
        }
    }
}