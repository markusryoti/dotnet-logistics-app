using LogisticsApp.Infrastructure;
using LogisticsApp.Modules.Inventory;
using LogisticsApp.Modules.Orders;
using LogisticsApp.Modules.Shipping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IInProcessDomainEventBus, DomainEventBus>();
builder.Services.AddSingleton<IInProcessIntegrationEventBus, InProcessIntegrationEventBus>();
builder.Services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

builder.Services.AddInventoryModule();
builder.Services.AddOrdersModule();
builder.Services.AddShippingModule();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));

app.MapGroup("/api/inventory").MapInventoryEndpoints();
app.MapGroup("/api/orders").MapOrdersEndpoints();
app.MapGroup("/api/shipping").MapShippingEndpoints();

app.Run();
