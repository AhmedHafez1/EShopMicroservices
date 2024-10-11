var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Services
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

app.MapGet("/", () => "Hello World!");

// Request Pipeline
app.MapReverseProxy();

app.Run();
