using MILAV;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR();

// Initialize real backend
var controller = new Controller();
// Inject backend
builder.Services.AddScoped(_ => controller);

var app = builder.Build();

//app.UseAuthorization();

app.MapHub<SignalRHub>("");

app.Run();
