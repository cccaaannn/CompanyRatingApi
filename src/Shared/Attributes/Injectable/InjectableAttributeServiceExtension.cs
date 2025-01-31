using System.Reflection;

namespace CompanyRatingApi.Shared.Attributes.Injectable;

public static class InjectableAttributeServiceExtension
{
    public static void AddInjectablesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var typesWithAttribute = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<InjectableAttribute>() != null);

        foreach (var type in typesWithAttribute)
        {
            var attribute = type.GetCustomAttribute<InjectableAttribute>();
            _ = attribute!.Lifetime switch
            {
                ServiceLifetime.Singleton => services.AddSingleton(type),
                ServiceLifetime.Scoped => services.AddScoped(type),
                ServiceLifetime.Transient => services.AddTransient(type),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}