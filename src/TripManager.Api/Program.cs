using TripManager.Api.OptionsSetup;
using TripManager.Application;
using TripManager.Domain;
using TripManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Services

var services = builder.Services;

services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

services.ConfigureOptions<JwtOptionsSetup>();
services.ConfigureOptions<JwtBearerOptionsSetup>();

#endregion

#region App

var app = builder.Build();


// app.MapGet("/test", async (ISender sender, CancellationToken cancellationToken = default) =>
//     {
//         var result = await sender.Send(new GetRandomTextQuery(), cancellationToken);
//         return Results.Ok(result);
//     })
//     .WithDisplayName("Test Endpoint")
//     .WithDescription("This is a test endpoint.")
//     .WithOpenApi();

app.UseInfrastructure();
app.Run();

#endregion