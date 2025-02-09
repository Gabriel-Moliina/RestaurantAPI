using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestaurantAPI.BackgroudServices;
using RestaurantAPI.Infra.Context;
using RestaurantAPI.IoC;

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

builder.Services.AddCors(options => options.AddPolicy(name: "restaurant", builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
}));

builder.Services.AddHttpContextAccessor();
builder.Services.RegistryDependency(builder.Configuration);
builder.Services.AddHostedService<TableReserveEmailConsumer>();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Example: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("restaurant");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
