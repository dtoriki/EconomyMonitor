using EconomyMonitor.Wpf.MVVM.Generic;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.Commands.Generic;

/// <summary>
/// Базовая реализация асинхронной команды <see cref="IAsyncCommand{TResult}"/>.
/// </summary>
/// <typeparam name="TResult">Тип возвращаемого командой значения.</typeparam>
/// <remarks>
/// <para>
/// Наследует <see cref="AsyncCommandBase"/>.
/// </para>
/// <para>
/// Реализует 
/// <see cref="IAsyncCommand{TResult}"/>.
/// </para>
/// </remarks>
/// <exception cref="ObjectDisposedException"/>
public abstract class AsyncCommandBase<TResult> : AsyncCommandBase, IAsyncCommand<TResult>
{
    /// <inheritdoc/>
    protected AsyncCommandBase() : base()
    {
    }

    /// <inheritdoc/>
    ITaskCompletion? IAsyncCommand.Execution => Execution;

    /// <inheritdoc cref="AsyncCommandBase.Execution"/>
    new public ITaskCompletion<TResult>? Execution
    {
        get
        {
            if (IsDisposed)
            {
                ThrowDisposed(this);
            }

            return base.Execution as ITaskCompletion<TResult>;
        }

        protected set
        {
            if (IsDisposed)
            {
                ThrowDisposed(this);
            }

            base.Execution = value;
        }
    }
}
