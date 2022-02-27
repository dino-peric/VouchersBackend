namespace VouchersBackend.Modules;

public interface IModule
{
    void MapEndpoints(WebApplication app);
    void DefineServices(IServiceCollection services);

    WebApplicationBuilder RegisterModule(WebApplicationBuilder builder);

    // TODO Expose middleware
    // 
}

public static class ModuleExtensions
{
    public static void AddModules(this WebApplicationBuilder builder, params Type[] scanMarkers)
    {
        var modules = new List<IModule>();

        modules.AddRange(typeof(IModule).Assembly
            .GetTypes()
            .Where(m => m.IsClass && m.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>());

        foreach (var endpointDefinition in modules)
        {
            endpointDefinition.RegisterModule(builder);
            endpointDefinition.DefineServices(builder.Services);
        }

        builder.Services.AddSingleton(modules as IReadOnlyCollection<IModule>);
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

