using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Clean_Arquitecture.Entities.Interfaces;
using Clean_Arquitecture.Repositories.EFCore.DataContext;
using Clean_Arquitecture.Repositories.EFCore.Repositories;
using Clean_Arquitecture.UseCases.CreateOrder;
using Proyecto_Clean_Arquitecture.UseCasesPorts.CreateOrder;
using Clean_Arquitecture.UseCases.Common.Validators;
using Clean_Arquitecture.Presenters;

namespace Clean_Arquitecture.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddClean_ArquitectureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Clean_ArquitectureContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Clean_ArquitectureDB")));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddValidatorsFromAssembly(typeof(CreateOrderValidator).Assembly);
            services.AddScoped<ICreateOrderInputPort, CreateOrderInteractor>();
            services.AddScoped<ICreateOrderOutputPort, CreateOrderPresenter>();

            return services;
        }
    }
}
