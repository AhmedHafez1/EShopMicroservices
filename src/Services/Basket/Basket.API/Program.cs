var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container


var app = builder.Build();

// Configure http request pipeline


app.Run();
