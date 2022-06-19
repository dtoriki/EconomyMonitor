using System.Collections;
using EconomyMonitor.Abstacts;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Primitives.Collections;

/// <summary>
/// Денежный поток.
/// </summary>
/// <typeparam name="TFlow">Тип денег в потоке.</typeparam>
/// <exception cref="ArgumentNullException"/>
public class MoneyFlow<TFlow> : IMoneyFlow<TFlow>
    where TFlow : class, IMoney
{
    private readonly ICollection<TFlow> _flow;

    /// <summary>
    /// Возвращает количество элементов в потоке.
    /// </summary>
    public int Count => _flow.Count;

    /// <summary>
    /// Возвращает значение, определяющее, предназначен ли поток только для чтения.
    /// </summary>
    /// <remarks>
    /// <see langword="true"/>, если поток предназначен только для чтения, иначе - <see langword="false"/>.
    /// </remarks>
    public bool IsReadOnly => _flow.IsReadOnly;

    /// <summary>
    /// Возвращает суммарное количество денег в потоке.
    /// </summary>
    public decimal Amount => _flow.Sum(x => x.Amount);

    /// <summary>
    /// Создаёт экземпляр денежного потока.
    /// </summary>
    /// <param name="flow">
    /// Перечисление денег, которые кладутся в поток.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="flow"/> оказывается <see langword="null"/>.
    /// </exception>
    public MoneyFlow(IEnumerable<TFlow> flow)
    {
        _ = ThrowIfArgumentNull(flow);

        _flow = flow.ToList();
    }

    /// <summary>
    /// Создаёт экземпляр денежного потока.
    /// </summary>
    public MoneyFlow() : this (Enumerable.Empty<TFlow>())
    {

    }

    /// <summary>
    /// Добавляет экземпляр денег в поток.
    /// </summary>
    /// <param name="item">Экземпляр типа денег.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="item"/> оказывается <see langword="null"/>.
    /// </exception>
    public void Add(TFlow item)
    {
        _ = ThrowIfArgumentNull(item);

        _flow.Add(item);
    }

    /// <summary>
    /// Очищеает поток.
    /// </summary>
    public void Clear() => _flow.Clear();

    /// <summary>
    /// Проверяет, находится ли <paramref name="item"/> в потоке.
    /// </summary>
    /// <param name="item">Экземпляр типа денег.</param>
    /// <returns>
    /// <see langword="true"/>, 
    /// если <paramref name="item"/> назодится в потоке, 
    /// иначе - <see langword="false"/>
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="item"/> оказывается <see langword="null"/>.
    /// </exception>
    public bool Contains(TFlow item)
    {
        _ = ThrowIfArgumentNull(item);

        return _flow.Contains(item);
    }

    /// <summary>
    /// Копирует элементы текущего потока в массив <paramref name="array"/>.
    /// </summary>
    /// <param name="array">Массив для копирования.</param>
    /// <param name="arrayIndex">Индекс, с которого начинается копирование.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="array"/> оказывается <see langword="null"/>.
    /// </exception>
    public void CopyTo(TFlow[] array, int arrayIndex)
    {
        _ = ThrowIfArgumentNull(array);

        _flow.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Удаляет экземпляр денег из потока.
    /// </summary>
    /// <param name="item">Экземпляр типа денег.</param>
    /// <returns>
    /// <see langword="true"/>, если экземпляр был удалён из потока,
    /// иначе - <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="item"/> оказывается <see langword="null"/>.
    /// </exception>
    public bool Remove(TFlow item)
    {
        _ = ThrowIfArgumentNull(item);

        return _flow.Remove(item);
    }

    /// <summary>
    /// Возвращает перечислитель по текущему экземпляру потока.
    /// </summary>
    /// <returns>Перечислитель по текущему экземпляру потока.</returns>
    public IEnumerator<TFlow> GetEnumerator() => _flow.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
