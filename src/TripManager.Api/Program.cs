using MediatR;
using TripManager.Application;
using TripManager.Application.Features.Tests.Queries;
using TripManager.Domain;
using TripManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Services

var services = builder.Services;

services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

#endregion

#region App

var app = builder.Build();


// app.MapGet("/test", async (ISender sender) =>
//     {
//         var result = await sender.Send(new GetRandomTextQuery());
//         return Results.Ok(result);
//     })
//     .WithDisplayName("Test Endpoint")
//     .WithDescription("This is a test endpoint.")
//     .WithOpenApi();

app.UseInfrastructure();
app.Run();

#endregion