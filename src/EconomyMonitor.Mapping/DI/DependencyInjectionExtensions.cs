using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Mapping.DI;
public static class DependencyInjectionExtensions
{
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
        Func<IConfigurationProvider, TMapper> implementationFactory)
            where TProfile : Profile, new()
            where TMapper : class, IMapper
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
