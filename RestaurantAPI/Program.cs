using Microsoft.EntityFrameworkCore;
using RestaurantAPI.IoC;
using RestaurantAPI.Infra.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EntityContext>(opt =>
{
    string connection = builder.Configuration.GetConnectionString("MySQL");
    opt.UseMySql(connection, ServerVersion.AutoDetect(connection));
});

builder.Services.RegistryDependency(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
