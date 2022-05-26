using AutoMapper;

namespace EconomyMonitor.Mapping.AutoMapper;

/// <summary>
/// Представляет тип сопоставления сущностей с объектами передачи данных.
/// </summary>
/// <remarks>Наследует <see cref="IMapper"/>.</remarks>
public interface IEntityWithDtoMapper : IMapper { } 
// ToDo: сделать отдельный маппер для Periods.
// ToDo: сделать в этом маппере методы маппинга с интерфейсами (Map<From, To>() where From, To: один интерфейс => ...
