using Clean_Arquitecture.Entities.Interfaces;
using Clean_Arquitecture.Repositories.EFCore.DataContext;
using Clean_Arquitecture.Repositories.EFCore.Repositories;
using Clean_Arquitecture.UseCases.Common.Behaviors;
using Clean_Arquitecture.UseCases.CreateOrder;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            services.AddMediatR(typeof(CreateOrderInteractor));
            services.AddValidatorsFromAssembly(typeof(CreateOrderValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
