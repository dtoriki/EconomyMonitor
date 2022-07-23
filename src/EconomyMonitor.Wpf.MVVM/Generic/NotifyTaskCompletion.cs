using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.Generic;

/// <summary>
/// Уведомляет об изменении состояния выполнения задачи <see cref="Task{TResult}"/>.
/// </summary>
/// <typeparam name="TResult"><inheritdoc cref="ITaskCompletion{TResult}"/></typeparam>
/// <remarks>
/// <para>
/// Наследует <see cref="NotifyTaskCompletion"/>.
/// </para>
/// <para>
/// Реализует 
/// <see cref="ITaskCompletion{TResult}"/>.
/// </para>
/// <para>
/// От этого класса нельзя унаследоваться.
/// </para>
/// </remarks>
/// <exception cref="ObjectDisposedException"/>
/// <exception cref="ArgumentNullException"/>
public sealed class NotifyTaskCompletion<TResult> : NotifyTaskCompletion, ITaskCompletion<TResult>
{
    private TResult? _result;

    /// <inheritdoc/>
    /// <remarks>
    /// Оборачивает <see cref="Task{TResult}.Result"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    public TResult? Result
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _result;
        }

        private set => _ = SetPropertyNotifiable(ref _result, value);
    }

    /// <summary>
    /// Создаёт экземпляр, уведомляющий об изменении состояния выполнения задачи <paramref name="task"/>.
    /// </summary>
    /// <param name="task"><see cref="Task{TResult}"/> для отслеживания состояния выполнения.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="task"/> оказался <see langword="null"/>.
    /// </exception>
    public NotifyTaskCompletion(Func<object?, CancellationToken, Task<TResult>> execution) : base(execution) => ThrowIfArgumentNull(execution);

    /// <inheritdoc/>
    protected override async Task WatchTaskAsync(Task executionTask)
    {
        await base.WatchTaskAsync(executionTask).ConfigureAwait(false);

        if (executionTask.IsCompletedSuccessfully)
        {
            Result = ((Task<TResult>)executionTask).Result;
        }
    }
}
