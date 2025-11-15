using Logistics.Shared;

var builder = WebApplication.CreateBuilder(args);

var logger = LoggerFactory.Create(loggingBuilder =>
{
    loggingBuilder.AddConsole();
}).CreateLogger("ModuleLoader");

builder.Services.RegisterModules(builder.Configuration, logger);
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IInProcessDomainEventBus, DomainEventBus>();
builder.Services.AddSingleton<IInProcessIntegrationEventBus, InProcessIntegrationEventBus>();

var app = builder.Build();

app.ApplyModuleMigrations();

app.MapGet("/", () => "Logistics API is running.");
app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapModuleEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
