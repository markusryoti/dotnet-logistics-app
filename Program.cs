using LogisticsApp.Modules.Inventory;
using LogisticsApp.Modules.Orders;
using LogisticsApp.Modules.Shipping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInventoryModule();
builder.Services.AddOrdersModule();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));

app.MapGroup("/api/inventory").MapInventoryEndpoints();
app.MapGroup("/api/orders").MapOrdersEndpoints();
app.MapGroup("/api/shipping").MapShippingEndpoints();

app.Run();
