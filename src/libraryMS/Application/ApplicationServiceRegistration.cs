using System.Reflection;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Application.Pipelines.Validation;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Abstraction;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Configurations;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Serilog.File;
using NArchitecture.Core.ElasticSearch;
using NArchitecture.Core.ElasticSearch.Models;
using NArchitecture.Core.Localization.Resource.Yaml.DependencyInjection;
using NArchitecture.Core.Mailing;
using NArchitecture.Core.Mailing.MailKit;
using NArchitecture.Core.Security.DependencyInjection;
using Application.Services.Authors;
using Application.Services.Borrows;
using Application.Services.Employees;
using Application.Services.Fines;
using Application.Services.Items;
using Application.Services.ItemAuthors;
using Application.Services.Libraries;
using Application.Services.LibraryMembers;
using Application.Services.Locations;
using Application.Services.Members;
using Application.Services.Payments;
using Application.Services.Publishers;
using Application.Services.Reports;
using Application.Services.Reservations;
using Application.Services.Reviews;
using Application.Services.UserOperationClaims;
using Application.Services.OperationClaims;
using Hangfire;
using Application.Services.MailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application;

public static class ApplicationServiceRegistration
{
    
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        MailSettings mailSettings,
        FileLogConfiguration fileLogConfiguration,
        ElasticSearchConfig elasticSearchConfig,
        NArchitecture.Core.Security.JWT.TokenOptions tokenOptions,
        IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
        configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
        configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
        configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
        configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
        configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<NArchitecture.Core.Mailing.IMailService, MailKitMailService>(_ => new MailKitMailService(mailSettings));
        services.AddSingleton<ILogger, SerilogFileLogger>(_ => new SerilogFileLogger(fileLogConfiguration));
        services.AddSingleton<IElasticSearch, ElasticSearchManager>(_ => new ElasticSearchManager(elasticSearchConfig));

        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<IUserOperationClaimService, UserOperationClaimManager>();
        services.AddScoped<IOperationClaimService, OperationClaimManager>();
        services.AddScoped<IAuthorService, AuthorManager>();
        services.AddScoped<IBorrowService, BorrowManager>();
        services.AddScoped<IEmployeeService, EmployeeManager>();
        services.AddScoped<IFineService,FineManager>();
        services.AddScoped<IItemService, ItemManager>();
        services.AddScoped<IItemAuthorService, ItemAuthorManager>();
        services.AddScoped<ILibraryService, LibraryManager>();
        services.AddScoped<ILibraryMemberService, LibraryMemberManager>();
        services.AddScoped<ILocationService, LocationManager>();
        services.AddScoped<IMemberService, MemberManager>();
        services.AddScoped<IPaymentService, PaymentManager>();
        services.AddScoped<IPublisherService, PublisherManager>();
        services.AddScoped<IReportService, ReportManager>();
        services.AddScoped<IReservationService, ReservationManager>();
        services.AddScoped<IReviewService, ReviewManager>();
        services.AddYamlResourceLocalization();
        services.AddSecurityServices<Guid, int, Guid>(tokenOptions);

        services.AddSingleton<Application.Services.MailService.IMailService, MailService>();

        services.AddHangfire(conf => conf
        .UseSqlServerStorage(configuration.GetConnectionString("HangfireDb")));
        services.AddHangfireServer();
        
        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
