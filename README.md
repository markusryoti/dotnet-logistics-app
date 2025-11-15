# Dotnet modular monolith

## Migrations

```bash
dotnet ef migrations add InitialOrders \
  --project Logistics.Modules.Orders \
  --startup-project Logistics.Api \
  --context OrderDbContext \
  --output-dir Infrastructure/Migrations
```
