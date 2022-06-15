//using System.Collections.ObjectModel;
//using System.Windows;
//using System.Windows.Input;
//using EconomyMonitor.Abstacts;
//using EconomyMonitor.Primitives.Enums;
//using EconomyMonitor.Services.UnitOfWork;
//using EconomyMonitor.Wpf.MVVM.Commands;
//using Microsoft.Extensions.DependencyInjection;
//using static EconomyMonitor.Helpers.ThrowHelper;

//namespace EconomyMonitor.Wpf.MVVM.ViewModels.Periods;

//internal class MonthDayViewModel : NotifyPropertyChangedBase, IMonthDay
//{
//    private int? _monthDay;
//    private MonthDayType _monthDayType;

//    public int? MonthDay
//    {
//        get => _monthDay;
//        set => _ = SetPropertyNotifiable(ref _monthDay, value);
//    }
//    public MonthDayType MonthDayType
//    {
//        get => _monthDayType;
//        set => _ = SetPropertyNotifiable(ref _monthDayType, value);
//    }

//    public MonthDayViewModel()
//    {

//    }
//}

//internal class SpendingQuotaViewModel : NotifyPropertyChangedBase, ISpendingQuota
//{
//    private decimal? _quota;
//    private bool _isNominal;
//    private decimal? _percent;


//    public bool IsPercent
//    {
//        get => _isNominal;
//        set => _ = SetPropertyNotifiable(ref _isNominal, value);
//    }

//    public decimal? QuotaValue
//    {
//        get => IsPercent
//                ? _percent
//                : _quota;
//        set
//        {
//            if (IsPercent)
//            {
//                _ = SetPropertyNotifiable(ref _percent, value);

//                return;
//            }

//            _ = SetPropertyNotifiable(ref _quota, value);
//        }
//    }

//    decimal? ISpendingQuota.Quota => _quota;

//    decimal? ISpendingQuota.Percent => _percent;

//    public SpendingQuotaViewModel()
//    {

//    }
//}

//internal sealed class AddDatePeriodConfigurationDialogViewModel :
//    NotifyPropertyChangedBase,
//    IDatePeriodConfiguration,
//    IDisposable,
//    IAsyncDisposable
//{
//    private readonly IServiceScopeFactory _scopeFactory;
//    private readonly IAsyncCommand _defaultDatePeriodConfigurationNotExistsCheck;
//    private readonly IAsyncCommand _createDatePeriodCommand;
//    private readonly IEnumerable<MonthDayType> _monthDayTypes;
//    //private readonly IEnumerable<MonthDayViewModel> _periodSplits;
//    //private readonly ObservableCollection<SpendingQuotaViewModel> _spendingQuotas;
//    private readonly ICommand _addSpendingQuotaCommand;
//    private bool _isDefault;
//    private bool _isOpen;
//    private bool _disposed;
//    private Visibility _isDeafultVisibility;
//    //private int? _monthDay;
//    //private MonthDayType _monthDayType;
//    private Visibility _monthDayVisibility;

//    public bool IsOpen
//    {
//        get
//        {
//            if (_disposed)
//            {
//                ThrowDisposed(this);
//            }

//            return _isOpen;
//        }
//        set
//        {
//            if (_disposed)
//            {
//                ThrowDisposed(this);
//            }

//            _ = SetPropertyNotifiable(ref _isOpen, value);
//        }
//    }

//    public IAsyncCommand DefaultDatePeriodConfigurationExistsCheck
//    {
//        get
//        {
//            if (_disposed)
//            {
//                _ = ThrowDisposed(this);
//            }

//            return _defaultDatePeriodConfigurationNotExistsCheck;
//        }
//    }
//    public IAsyncCommand CreateDatePeriodCommand
//    {
//        get
//        {
//            if (_disposed)
//            {
//                ThrowDisposed(this);
//            }

//            return _createDatePeriodCommand;
//        }
//    }

//    public ICommand AddSpendingQuotaCommand
//    {
//        get
//        {
//            if (_disposed)
//            {
//                ThrowDisposed(this);
//            }

//            return _addSpendingQuotaCommand;
//        }
//    }

//    public Visibility IsDeafultVisibility
//    {
//        get
//        {
//            if (_disposed)
//            {
//                ThrowDisposed(this);
//            }

//            return _isDeafultVisibility;
//        }
//        set
//        {
//            if (_disposed)
//            {
//                ThrowDisposed(this);
//            }

//            _ = SetPropertyNotifiable(ref _isDeafultVisibility, value);
//        }
//    }
//    public Visibility MonthDayVisibility
//    {
//        get
//        {
//            if (_disposed)
//            {
//                ThrowDisposed(this);
//            }

//            return _monthDayVisibility;
//        }
//        set
//        {
//            if (_disposed)
//            {
//                ThrowDisposed(this);
//            }

//            _ = SetPropertyNotifiable(ref _monthDayVisibility, value);
//        }
//    }

//    //public bool IsDefault
//    //{
//    //    get
//    //    {
//    //        if (_disposed)
//    //        {
//    //            ThrowDisposed(this);
//    //        }

//    //        return _isDefault;
//    //    }
//    //}

//    //public int? MonthDay
//    //{
//    //    get
//    //    {
//    //        if (_disposed)
//    //        {
//    //            ThrowDisposed(this);
//    //        }

//    //        return _monthDay;
//    //    }
//    //    set
//    //    {
//    //        if (_disposed)
//    //        {
//    //            ThrowDisposed(this);
//    //        }

//    //        _ = SetPropertyNotifiable(ref _monthDay, value);
//    //    }
//    //}
//    //public MonthDayType MonthDayType
//    //{
//    //    get
//    //    {
//    //        if (_disposed)
//    //        {
//    //            ThrowDisposed(this);
//    //        }

//    //        MonthDayVisibility = _monthDayType == MonthDayType.Other
//    //            ? Visibility.Visible
//    //            : Visibility.Collapsed;

//    //        OnPropertyChanged(nameof(MonthDayVisibility));

//    //        return _monthDayType;
//    //    }
//    //    set
//    //    {
//    //        if (_disposed)
//    //        {
//    //            ThrowDisposed(this);
//    //        }

//    //        _ = SetPropertyNotifiable(ref _monthDayType, value);
//    //    }
//    //}

//    //public IEnumerable<MonthDayViewModel> PeriodSplits
//    //{
//    //    get
//    //    {
//    //        if (_disposed)
//    //        {
//    //            ThrowDisposed(this);
//    //        }

//    //        return _periodSplits;
//    //    }
//    //}
//    //public IEnumerable<SpendingQuotaViewModel> SpendingQuotas
//    //{
//    //    get
//    //    {
//    //        if (_disposed)
//    //        {
//    //            ThrowDisposed(this);
//    //        }

//    //        return _spendingQuotas;
//    //    }
//    //}
//    public IEnumerable<MonthDayType> MonthDayTypes => _monthDayTypes;

//    //IEnumerable<IMonthDay> IDatePeriodConfiguration.PeriodSplits => PeriodSplits;
//    //IEnumerable<ISpendingQuota> IDatePeriodConfiguration.SpendingQuotas => SpendingQuotas;


//    public AddDatePeriodConfigurationDialogViewModel(IServiceScopeFactory scopeFactory)
//    {
//        _scopeFactory = scopeFactory;
//        _createDatePeriodCommand = new RelayAsyncCommand(ExecuteCreateDatePeriodConfigurationAsync);
//        _defaultDatePeriodConfigurationNotExistsCheck = new RelayAsyncCommand(ExecuteDefaultDatePeriodConfigurationNotExistsCheck);

//        _addSpendingQuotaCommand = new RelayCommand(ExecuteAddSpendingQuotaCommand);
        
//        _monthDayTypes = new ObservableCollection<MonthDayType>(Enum.GetValues<MonthDayType>().Where(x => x != 0));
//        _periodSplits = new ObservableCollection<MonthDayViewModel>();
//        _spendingQuotas = new ObservableCollection<SpendingQuotaViewModel>();
//    }

//    public void Dispose()
//    {
//        if (_disposed)
//        {
//            return;
//        }

//        if (_defaultDatePeriodConfigurationNotExistsCheck is IDisposable disposable)
//        {
//            disposable.Dispose();
//            _disposed = true;
//            GC.SuppressFinalize(this);
//        }
//    }

//    public async ValueTask DisposeAsync()
//    {
//        if (_disposed)
//        {
//            return;
//        }

//        if (_defaultDatePeriodConfigurationNotExistsCheck is IAsyncDisposable disposable)
//        {
//            await disposable.DisposeAsync();
//            _disposed = true;
//            GC.SuppressFinalize(this);
//        }
//    }

//    private async Task ExecuteDefaultDatePeriodConfigurationNotExistsCheck(object? parameter, CancellationToken cancellationToken)
//    {
//        if (_disposed)
//        {
//            _ = ThrowDisposed(this);
//        }

//        await using AsyncServiceScope scope = _scopeFactory.CreateAsyncScope();
//        IDatePeriodConfigurationUnitOfWork? datePeriodConfigurationUnitOfWork = scope.ServiceProvider
//            .GetRequiredService<IDatePeriodConfigurationUnitOfWork>();

//        IDatePeriodConfiguration? defaultConfiguration = await datePeriodConfigurationUnitOfWork
//            .GetDefaultConfigurationAsync(cancellationToken);

//        IsOpen = defaultConfiguration is null;
//        _isDefault = defaultConfiguration is not null;
//        _isDeafultVisibility = defaultConfiguration is null
//            ? Visibility.Hidden
//            : Visibility.Visible;
//    }

//    private async Task ExecuteCreateDatePeriodConfigurationAsync(object? parameter, CancellationToken cancellationToken)
//    {
//        if (_disposed)
//        {
//            ThrowDisposed(this);
//        }

//        await using AsyncServiceScope scope = _scopeFactory.CreateAsyncScope();
//        IDatePeriodConfigurationUnitOfWork? datePeriodConfigurationUnitOfWork = scope.ServiceProvider
//            .GetRequiredService<IDatePeriodConfigurationUnitOfWork>();

//        await datePeriodConfigurationUnitOfWork.CreateConfigurationAsync(this, cancellationToken);

//        IsOpen = false;
//    }

//    private void ExecuteAddSpendingQuotaCommand(object? parameter)
//    {
//        _spendingQuotas.Add(new SpendingQuotaViewModel());
//    }

//}
