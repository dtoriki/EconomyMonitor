using AutoMapper;
using EconomyMonitor.Data;
using EconomyMonitor.Data.DI;
using EconomyMonitor.Data.EfSets;
using EconomyMonitor.Mapping.AutoMapper.DatePeriod;
using EconomyMonitor.Services.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;
using IMapperConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace EconomyMonitor.DI.Extensions;

/// <summary>
/// Содержит методы расширения <see cref="IServiceCollection"/> 
/// для конфигурации и добавлении в <see cref="IServiceCollection"/>
/// со временем жизни <see cref="ServiceLifetime.Scoped"/>.
/// </summary>
/// <exception cref="NullReferenceException"/>
public static class ServiceCollectionScopedExtensions
{
    /// <summary>
    /// Конфигурирует <see cref="IAppRepository"/>, используя поставщик данных Sqlite 
    /// с временем существования <see cref="ServiceLifetime.Scoped"/>
    /// и добавляет его в <paramref name="services"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    /// <remarks>
    /// <para>
    /// Методом <see cref="ServiceCollectionExtensions.GetConnectionString(IServiceCollection)"/>
    /// пытается полчить строку подключения. Если строка подключения не найдена, то вызывает <see cref="ArgumentNullException"/>.
    /// </para>
    /// <para>
    /// Методом <see cref="ConfigureAppRepositoryExtensions.ConfigureAppRepositoryScoped(IServiceCollection, string)"/>
    /// конфигурирует экземпляр <see cref="IAppRepository"/> со временем жизни <see cref="ServiceLifetime.Scoped"/>
    /// и помещает его в <paramref name="services"/>.
    /// </para>
    /// </remarks>
    /// <exception cref="NullReferenceException">
    /// Возникает, если не удалось найти строку подключения.
    /// </exception>
    public static IServiceCollection ConfigureSqliteAppRepository(this IServiceCollection services)
    {
        string? connectionString = services.GetConnectionString();
        if (ThrowIfNull(connectionString))
        {
            return services;
        }

        services.ConfigureAppRepositoryScoped(connectionString);

        return services;
    }

    /// <summary>
    /// Конфигурирует экземпляр типа работы с хранилищем данных <see cref="IDatePeriodsUnitOfWork"/>, 
    /// которое реализует <see cref="IDatePeriodSet"/>
    /// и добавляет его в <paramref name="services"/> c временем существования <see cref="ServiceLifetime.Scoped"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    /// <remarks>
    /// Перед конфигурацией типа работы с хранилищем данных необходимо, 
    /// чтобы были сконфигурированны <see cref="IAppRepository"/>
    /// и <see cref="IDatePeriodMapper"/>.
    /// </remarks>
    public static IServiceCollection ConfigureDatePeriodsUnitOfWorkScoped(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            IAppRepository repository = provider.GetRequiredService<IAppRepository>();
            IDatePeriodMapper mapper = provider.GetRequiredService<IDatePeriodMapper>();

            return IDatePeriodsUnitOfWork.Create(repository, mapper);
        });

        return services;
    }

    /// <summary>
    /// Конфигурирует экземпляры типов работы с хранилищем данным
    /// и добавляет их в <paramref name="services"/> с временем жизни <see cref="ServiceLifetime.Scoped"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    /// <remarks>
    /// Конфигурирует:
    /// <list type="bullet">
    /// <item><see cref="IDatePeriodsUnitOfWork"/> 
    /// методом <see cref="ConfigureDatePeriodsUnitOfWorkScoped(IServiceCollection)"/>.</item>
    /// </list>
    /// <para>
    /// Перед конфигурацией типа работы с хранилищем данных необходимо, 
    /// чтобы были сконфигурированны <see cref="IAppRepository"/>
    /// и <see cref="IDatePeriodMapper"/>.
    /// </para>
    /// </remarks>
    public static IServiceCollection ConfigureUnitsOfWorkScoped(this IServiceCollection services)
    {
        services.ConfigureDatePeriodsUnitOfWorkScoped();

        return services;
    }

    /// <summary>
    /// Конфигурирует тип сопоставления данных 
    /// <see cref="IDatePeriodMapper"/>
    /// и добавляет его в <paramref name="services"/> 
    /// c временем существования <see cref="ServiceLifetime.Scoped"/>.
    /// </summary>
    /// <param name="services">Коллеция сервисов.</param>
    /// <returns>Коллеция сервисов.</returns>
    /// <remarks>
    /// Для конфигурации типа сопоставления данных использует метод 
    /// <see cref="ConfigureMapperScoped{TProfile, TMapper}(IServiceCollection, Func{IMapperConfigurationProvider, TMapper})"/>
    /// </remarks>
    public static IServiceCollection ConfigureMappers(this IServiceCollection services)
    {
        services.ConfigureMapperScoped<DatePeriodMapProfile, IDatePeriodMapper>(p => new DatePeriodMapper(p));
        
        return services;
    }

    /// <summary>
    /// Конфигурирует тип сопоставления данных <paramref name="services"/>
    /// и добавляет его в <paramref name="services"/> 
    /// c временем существования <see cref="ServiceLifetime.Scoped"/>.
    /// </summary>
    /// <typeparam name="TProfile">Тип профиля сопоставления.</typeparam>
    /// <typeparam name="TMapper">Тип сопоставления данных.</typeparam>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="implementationFactory">Фабрика реализации типа осопоставления.</param>
    /// <returns>Коллекция сервисов.</returns>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="implementationFactory"/> является <see langword="null"/>.
    /// </exception>
    public static IServiceCollection ConfigureMapperScoped<TProfile, TMapper>(
        this IServiceCollection services,
        Func<IMapperConfigurationProvider, TMapper> implementationFactory) 
            where TProfile : Profile, new() 
            where TMapper : class
    {
        _ = ThrowIfArgumentNull(implementationFactory);

        var config = new MapperConfiguration(cfg =>
        {
            var profile = new TProfile();
            cfg.AddProfile(profile);
        });

        TMapper mapper = implementationFactory(config);

        services.AddScoped(sp => mapper);

        return services;
    }
}
