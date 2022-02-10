var builder = WebApplication.CreateBuilder(args);
var app = builder.AddServices();

app.MapGet("/", () => "Hello World!");

app.Run();
