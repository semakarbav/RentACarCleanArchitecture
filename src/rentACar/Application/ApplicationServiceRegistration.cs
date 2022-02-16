using Application.Features.AdditionalServices.Rules;
using Application.Features.Brands.Rules;
using Application.Features.CarDamages.Rules;
using Application.Features.Cars.Rules;
using Application.Features.Colors.Rules;
using Application.Features.CorporateCustomers.Rules;
using Application.Features.Fuels.Rules;
using Application.Features.IndividualCustomers.Rules;
using Application.Features.Invoices.Rules;
using Application.Features.Maintenances.Rules;
using Application.Features.Models.Rules;
using Application.Features.OperationClaims.Rules;
using Application.Features.Payments.Rules;
using Application.Features.Rentals.Rules;
using Application.Features.Tranmissions.Rules;
using Application.Features.Users.Rules;
using Application.Services.AdditionalServiceForRentalsServices;
using Application.Services.AuthService;
using Application.Services.FindexScore;
using Application.Services.PosService;
using Application.Services.UserServices;
using Core.Application.Adapter;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.CrossCuttingConcerns.SeriLog;
using Core.CrossCuttingConcerns.SeriLog.Loggers;
using Core.ElasticSearch;
using Core.Mailing;
using Core.Mailing.MailkitImplementations;
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
            services.AddSingleton<IElasticSearch,ElasticSearchManager>();

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
            services.AddScoped<AdditionalServiceBusinessRules>();
            services.AddScoped<CarDamageBusinessRules>();
            services.AddScoped<PaymentBusinessRules>();

            services.AddScoped<IFindexScoreAdapterService, FindexScoreAdapterManager>();
            services.AddSingleton<IMailService, MailkitMailService>(); 
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdditionalServiceForRentalsService, AdditionalServiceForRentalsServices>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPosService, FakePosServiceAdapterManager>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddSingleton<LoggerServiceBase, FileLogger>();

            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));



            return services;
        }
    }
}
