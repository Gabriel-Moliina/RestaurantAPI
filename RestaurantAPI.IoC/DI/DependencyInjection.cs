using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Application.Application;
using RestaurantAPI.Domain.Builder;
using RestaurantAPI.Domain.Builder.ReservationBuilder;
using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Builder;
using RestaurantAPI.Domain.Interface.Email;
using RestaurantAPI.Domain.Interface.Messaging;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.Interface.Token;
using RestaurantAPI.Domain.Mapper;
using RestaurantAPI.Domain.Notification;
using RestaurantAPI.Domain.Validator.Restaurant;
using RestaurantAPI.Domain.Validator.Table;
using RestaurantAPI.Domain.Validator.User;
using RestaurantAPI.Infra.Context;
using RestaurantAPI.Infra.Email.Email;
using RestaurantAPI.Infra.Email.Sender;
using RestaurantAPI.Infra.Repository;
using RestaurantAPI.Infra.Security.Token;
using RestaurantAPI.Messaging.Sender;
using RestaurantAPI.Service.Services;

namespace RestaurantAPI.IoC
{
    public static class DependencyInjection
    {
        public static void RegistryDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext(configuration)
                .AddValidators()
                .AddNotifications()
                .AddAutoMapper()
                .AddAuthentication(configuration)
                .AddApplication()
                .AddServices()
                .AddMessagingRabbitMQ(configuration)
                .AddBuilders()
                .AddRepository();
        }

        private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<EntityContext>(opt =>
            {
                string connection = configuration.GetConnectionString("MySQL");
                opt.UseMySql(connection, ServerVersion.AutoDetect(connection));
            });
        }

        private static IServiceCollection AddRepository(this IServiceCollection services)
        {
            return services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRestaurantRepository, RestaurantRepository>()
                .AddScoped<ITableRepository, TableRepository>()
                .AddScoped<IReservationRepository, ReservationRepository>();
        }

        private static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services.AddScoped<IUserApplication, UserApplication>()
                .AddScoped<IRestaurantApplication, RestaurantApplication>()
                .AddScoped<ITableApplication, TableApplication>()
                .AddScoped<IReservationApplication, ReservationApplication>();
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<IUserService, UserService>()
                .AddScoped<IRestaurantService, RestaurantService>()
                .AddScoped<ITableService, TableService>()
                .AddScoped<IReservationService, ReservationService>();
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserCreateDTO>, UserCreateValidator>();
            services.AddScoped<IValidator<UserLoginDTO>, UserLoginValidator>();
            services.AddScoped<IValidator<RestaurantSaveDTO>, RestaurantSaveValidator>();
            services.AddScoped<IValidator<RestaurantDeleteDTO>, RestaurantDeleteValidator>();
            services.AddScoped<IValidator<TableSaveDTO>, TableSaveValidator>();
            services.AddScoped<IValidator<TableReservationDTO>, TableReservationValidator>();
            services.AddScoped<IValidator<TableCancelReservationDTO>, TableReservationCancelValidator>();
            services.AddScoped<IValidator<TableReleaseDTO>, TableReleaseValidator>();
            return services;
        }

        private static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            return services.AddScoped<INotification, NotificationContext>();
        }

        private static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMapper));
            services.AddAutoMapper(typeof(RestaurantMapper));
            services.AddAutoMapper(typeof(TableMapper));
            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITokenService, TokenService>();

            var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:SecretKey"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        private static IServiceCollection AddMessagingRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IEmailSettings, EmailSettings>();
            services.AddSingleton<IRabbitMQSettings, RabbitMQSettings>();
            services.AddTransient<IRabbitMQSender, RabbitMQSender>();
            return services;
        }
        
        private static IServiceCollection AddBuilders(this IServiceCollection services)
        {
            services.AddScoped<IReservationBuilder, ReservationBuilder>();
            services.AddScoped<ITableReservationResponseBuilder, TableReservationResponseBuilder>();
            services.AddScoped<IEmailBuilder, EmailBuilder>();
            return services;
        }
    }
}
