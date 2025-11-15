using Logistics.Shared;
using System.Reflection;

public static class ModuleLoader
{
    private static readonly List<IModule> _loadedModules = new();

    public static void RegisterModules(this IServiceCollection services, IConfiguration config, ILogger logger)
    {
        logger.LogInformation("Registering modules...");

        var baseDir = AppContext.BaseDirectory;
        logger.LogInformation("Probing modules in {BaseDir}", baseDir);

        // Find candidate dlls in the output folder that start with 'Logistics.Modules'
        var dllFiles = Directory.GetFiles(baseDir, "Logistics.Modules*.dll", SearchOption.TopDirectoryOnly);

        foreach (var dll in dllFiles)
        {
            try
            {
                var fileName = Path.GetFileName(dll);
                logger.LogInformation("Attempting to load assembly from {Dll}", fileName);

                // Skip if already loaded
                var already = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a => string.Equals(a.GetName().Name, Path.GetFileNameWithoutExtension(fileName), StringComparison.OrdinalIgnoreCase));

                Assembly asm;
                if (already != null)
                {
                    asm = already;
                }
                else
                {
                    asm = Assembly.LoadFrom(dll);
                }

                logger.LogInformation("Loaded assembly {Assembly}", asm.FullName);

                // Find IModule implementations
                var moduleTypes = asm.GetTypes()
                    .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsAbstract && t.IsClass)
                    .ToList();

                foreach (var mt in moduleTypes)
                {
                    try
                    {
                        logger.LogInformation("Found module type {ModuleType}", mt.FullName);

                        if (Activator.CreateInstance(mt) is IModule module)
                        {
                            module.RegisterServices(services, config);
                            _loadedModules.Add(module);
                            logger.LogInformation("Registered module {ModuleType}", mt.FullName);
                        }
                        else
                        {
                            logger.LogWarning("Could not create module instance of type {ModuleType}", mt.FullName);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error creating/registering module type {ModuleType}", mt.FullName);
                    }
                }
            }
            catch (ReflectionTypeLoadException rtle)
            {
                logger.LogError(rtle, "Failed to load types from assembly {Dll}", dll);
                foreach (var loaderEx in rtle.LoaderExceptions ?? Array.Empty<Exception>())
                {
                    logger.LogError(loaderEx, "Loader exception");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load assembly {Dll}", dll);
            }
        }

        logger.LogInformation("Module registration complete. Loaded {Count} modules.", _loadedModules.Count);
    }

    public static void MapModuleEndpoints(this WebApplication app)
    {
        app.Logger.LogInformation("Mapping module endpoints...");

        foreach (var module in _loadedModules)
        {
            try
            {
                module.MapEndpoints(app);
                app.Logger.LogInformation("Mapped endpoints for {Module}", module.GetType().FullName);
            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "Error mapping endpoints for {Module}", module.GetType().FullName);
            }
        }
    }

    public static void ApplyModuleMigrations(this WebApplication app)
    {
        app.Logger.LogInformation("Applying module migrations...");

        foreach (var module in _loadedModules)
        {
            try
            {
                module.ApplyMigrations(app);
                app.Logger.LogInformation("Applied migrations for {Module}", module.GetType().FullName);
            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "Error applying migrations for {Module}", module.GetType().FullName);
            }
        }
    }
}
