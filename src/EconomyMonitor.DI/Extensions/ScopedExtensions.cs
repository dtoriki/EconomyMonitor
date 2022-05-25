using AutoMapper;
using EconomyMonitor.Data;
using EconomyMonitor.Data.DI;
using EconomyMonitor.Mapping.AutoMapper;
using EconomyMonitor.Services.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;
using IMapperConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace EconomyMonitor.DI.Extensions;

/// <summary>
/// Provides adds for scope services.
/// </summary>
public static class ScopedExtensions
{
    /// <summary>
    /// Configures scoped <see cref="IEconomyMonitorRepository"/> with Sql Lite provider.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection ConfigureSqliteEconomyMonitorRepository(this IServiceCollection services)
    {
        string? connectionString = services.GetConnectionString();
        if (ThrowIfNull(connectionString))
        {
            return services;
        }

        services.ConfigureEconomyMonitorRepositoryScoped(connectionString);

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
    /// <item><see cref="IDatePeriodsUnitOfWork"/>.</item>
    /// </list>
    /// </remarks>
    public static IServiceCollection ConfigureUnitsOfWorkScoped(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            IEconomyMonitorRepository repository = provider.GetRequiredService<IEconomyMonitorRepository>();
            IEntityWithDtoMapper mapper = provider.GetRequiredService<IEntityWithDtoMapper>();

            return IDatePeriodsUnitOfWork.Create(repository, mapper);
        });

        return services;
    }

    /// <summary>
    /// Configures scoped <see cref="IEntityWithDtoMapper"/> mapper into <paramref name="services"/>.
    /// </summary>
    /// <param name="services">Services collection.</param>
    /// <returns>Services collection.</returns>
    public static IServiceCollection ConfigureEntityWithDtoMappers(this IServiceCollection services)
    {
        services.ConfigureMapperScoped<EntityWithDtoProfile, IEntityWithDtoMapper>(p => new EntityWithDtoMapper(p));

        return services;
    }

    /// <summary>
    /// Configures scoped mapper and adds it into <paramref name="services"/>
    /// </summary>
    /// <typeparam name="TProfile">Type of mapper profile.</typeparam>
    /// <typeparam name="TMapper">Type of mapper.</typeparam>
    /// <param name="services">Services collection.</param>
    /// <param name="implementationFactory">Factory for implement instance.</param>
    /// <returns>Services collection.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection ConfigureMapperScoped<TProfile, TMapper>(
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
