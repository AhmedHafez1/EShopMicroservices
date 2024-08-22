var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
var assemply = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assemply);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Default")!);
    opts.Schema.For<ShoppingCart>().Identity(basket => basket.UserName);
}).UseLightweightSessions();

builder.Services.AddCarter();

var app = builder.Build();

// Configure http request pipeline
app.MapCarter();

app.Run();
