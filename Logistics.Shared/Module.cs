namespace Logistics.Shared;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

public interface IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration config);
    public void MapEndpoints(IEndpointRouteBuilder endpoints);
}
