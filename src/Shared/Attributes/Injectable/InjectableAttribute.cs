namespace CompanyRatingApi.Shared.Attributes.Injectable;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class InjectableAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; }

    public InjectableAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        Lifetime = lifetime;
    }
}
