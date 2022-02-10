using Application.Features.Brands.Rules;
using Application.Features.Cars.Rules;
using Application.Features.Colors.Rules;
using Application.Features.CorporateCustomers.Rules;
using Application.Features.Fuels.Rules;
using Application.Features.IndividualCustomers.Rules;
using Application.Features.Invoices.Rules;
using Application.Features.Maintenances.Rules;
using Application.Features.Models.Rules;
using Application.Features.OperationClaims.Rules;
using Application.Features.Rentals.Rules;
using Application.Features.Tranmissions.Rules;
using Application.Features.Users.Rules;
using Application.Services.AuthService;
using Application.Services.Manager;
using Application.Services.UserServices;
using Core.Application.Adapter;
using Core.Application.Pipilines.Logging;
using Core.Application.Pipilines.Validation;
using Core.CrossCuttingConcerns.SeriLog;
using Core.CrossCuttingConcerns.SeriLog.Loggers;
using Core.Security.Jwt;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddSingleton<LoggerServiceBase,FileLogger>();

            services.AddScoped<BrandBusinessRules>();
            services.AddScoped<ModelBusinessRules>();
            services.AddScoped<CarBusinessRules>();
            services.AddScoped<ColorBusinessRules>();
            services.AddScoped<FuelBusinessRules>();
            services.AddScoped<TransmissionBusinessRules>();
            services.AddScoped<RentalBusinessRules>();
            services.AddScoped<MaintenanceBusinessRules>();
            services.AddScoped<CorporateCustomerBusinessRules>();
            services.AddScoped<IndividualCustomerBusinessRules>();
            services.AddScoped<InvoiceBusinessRules>();
            services.AddScoped<UserBusinessRules>();
            services.AddScoped<OperationClaimBusinessRules>();

            services.AddScoped<IFindexScoreAdapterService, FindexScoreAdapterManager>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenHelper, JwtHelper>();

            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));


            return services;
        }
    }
}
