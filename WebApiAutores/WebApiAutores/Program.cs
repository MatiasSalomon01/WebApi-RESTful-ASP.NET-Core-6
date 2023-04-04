using WebApiAutores;

var builder = WebApplication.CreateBuilder(args);

//Configurar Servicios
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

//Configurar Middleware
var app = builder.Build();
startup.Configure(app, app.Environment);

app.Run();
