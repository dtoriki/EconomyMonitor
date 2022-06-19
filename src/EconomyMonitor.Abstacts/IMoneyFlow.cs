namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип денежного потока.
/// </summary>
/// <typeparam name="TFlow"></typeparam>
public interface IMoneyFlow<TFlow> : ICollection<TFlow>, IMoney
    where TFlow : class, IMoney
{

}
