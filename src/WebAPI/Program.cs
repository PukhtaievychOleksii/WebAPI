var builder = WebApplication.CreateBuilder(args);
//DI IoC Container configuration
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>();

var app = builder.Build();

//Middlewares (request pipeline) configuration

app.MapControllers();

app.Run();

