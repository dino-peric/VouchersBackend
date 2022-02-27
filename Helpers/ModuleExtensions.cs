namespace VouchersBackend.Modules;

public interface IModule
{
    void MapEndpoints(WebApplication app);
    void DefineServices(IServiceCollection services);

    // TODO Expose middleware
    // 
}

public static class ModuleExtensions
{
    public static void AddModules(this IServiceCollection services, params Type[] scanMarkers)
    {
        var modules = new List<IModule>();

        foreach (var scanMarker in scanMarkers)
        {
            modules.AddRange(
                scanMarker.Assembly.ExportedTypes
                .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
                .Select(Activator.CreateInstance).Cast<IModule>()
                );
        }

        foreach (var endpointDefinition in modules)
        {
            endpointDefinition.DefineServices(services);
        }

        services.AddSingleton(modules as IReadOnlyCollection<IModule>);
    }

    public static void MapEndpoints(this WebApplication app)
    {
        var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IModule>>();

        foreach (var definition in definitions)
        {
            definition.MapEndpoints(app);
        }
    }
}

