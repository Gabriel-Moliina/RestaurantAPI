using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAPI.Application.Application;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.Mapper;
using RestaurantAPI.Domain.Notification;
using RestaurantAPI.Domain.Validator.User;
using RestaurantAPI.Infra.Repository;
using RestaurantAPI.Service.Services;

namespace RestaurantAPI.IoC
{
    public static class DependencyInjection
    {
        public static void RegistryDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidators()
                .AddNotifications()
                .AddAutoMapper()
                .AddApplication()
                .AddServices()
                .AddRepository();
        }

        private static IServiceCollection AddRepository(this IServiceCollection services)
        {
            return services.AddScoped<IUserRepository, UserRepository>();
        }

        private static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services.AddScoped<IUserApplication, UserApplication>();
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<IUserService, UserService>();
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserCreateDTO>, UserCreateValidator>();
            return services;
        }

        private static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            return services.AddScoped<INotification, NotificationContext>();
        }

        private static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMapper));
            return services;
        }
    }
}
