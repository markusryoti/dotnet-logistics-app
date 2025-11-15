namespace Logistics.Shared;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

public interface IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration config);
    public void MapEndpoints(IEndpointRouteBuilder endpoints);
    public void ApplyMigrations(IApplicationBuilder app);
}
