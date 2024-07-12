var builder = WebApplication.CreateBuilder(args);
//DI IoC Container configuration
builder.Services.AddScoped<BookService>();
builder.Services.AddControllers();

var app = builder.Build();

//Middlewares (request pipeline) configuration

app.MapControllers();

app.Run();

