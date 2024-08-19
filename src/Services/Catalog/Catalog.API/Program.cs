
using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
var assemply = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assemply);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assemply);

builder.Services.AddCarter();
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Default")!);
}).UseLightweightSessions();


var app = builder.Build();

// Configure http request pipeline
app.MapCarter();

app.Run();
