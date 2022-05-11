using AutoMapper;
using EconomyMonitor.Data;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Mapping.AutoMapper;
using EconomyMonitor.Services.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;
using IMapperConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace EconomyMonitor.DI.Extensions;

/// <summary>
/// Provides adds for scope services.
/// </summary>
public static class Scoped
{
    private static readonly string DefaultConnectionStringName = "DefaultConnectionString";

    /// <summary>
    /// Adds scoped <see cref="IEconomyMonitorRepository"/> with Sql Lite provider.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddSqlLiteEconomyMonitorRepositoryScoped(this IServiceCollection services)
    {
        IConfiguration configuration = services.BuildServiceProvider()
            .GetRequiredService<IConfiguration>();

        string connectionName = DIOptions.ConnectionStringName ?? DefaultConnectionStringName;
        string? connectionString = configuration.GetConnectionString(connectionName);
        if (ThrowIfNull(connectionString))
        {
            return services;
        }

        DbContextOptions options = new DbContextOptionsBuilder()
            .UseSqlite(connectionString)
            .EnableThreadSafetyChecks()
            .Options;

        services.AddScoped<IRepository>(_ => IEconomyMonitorRepository.Create(options));
        services.AddScoped(_ => IEconomyMonitorRepository.Create(options));

        return services;
    }

    /// <summary>
    /// Configures scoped unit of works.
    /// </summary>
    /// <param name="services">Services collection.</param>
    /// <returns>Services collection</returns>
    /// <remarks>
    /// Configures:
    /// <list type="bullet">
    /// <item><see cref="IPeriodsUnitOfWork"/>.</item>
    /// </list>
    /// </remarks>
    public static IServiceCollection ConfigureUnitsOfWorkScoped(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            IEconomyMonitorRepository repository = provider.GetRequiredService<IEconomyMonitorRepository>();
            IEntityWithDtoMapper mapper = provider.GetRequiredService<IEntityWithDtoMapper>();

            return IPeriodsUnitOfWork.Create(repository, mapper);
        });

        return services;
    }

    /// <summary>
    /// Adds <see cref="IEntityWithDtoMapper"/> mapper into <paramref name="services"/>.
    /// </summary>
    /// <param name="services">Services collection.</param>
    /// <returns>Services collection.</returns>
    public static IServiceCollection AddEntityWithDtoMappers(this IServiceCollection services)
    {
        services.ConfigureMapper<EntityWithDtoProfile, IEntityWithDtoMapper>(p => new EntityWithDtoMapper(p));

        return services;
    }

    /// <summary>
    /// Configures mapper and adds it into <paramref name="services"/>
    /// </summary>
    /// <typeparam name="TProfile">Type of mapper profile.</typeparam>
    /// <typeparam name="TMapper">Type of mapper.</typeparam>
    /// <param name="services">Services collection.</param>
    /// <param name="implementationFactory">Factory for implement instance.</param>
    /// <returns>Services collection.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection ConfigureMapper<TProfile, TMapper>(
        this IServiceCollection services,
        Func<IMapperConfigurationProvider, TMapper> implementationFactory) where TProfile : Profile, new() where TMapper : class, IMapper
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
