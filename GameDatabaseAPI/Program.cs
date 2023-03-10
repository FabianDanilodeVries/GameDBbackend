using DBGameDatabaseContextDB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GameDatabaseContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var allowOrigins = "_allowOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost/*", "https://localhost/*", "https://yc2209firstruncsharpskillsappapi20221013141147.azurewebsites.net/*", "https://zealous-smoke-053ac0c03.1.azurestaticapps.net/*").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(allowOrigins);

app.Run();
