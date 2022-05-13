using AutoMapper;
using EconomyMonitor.Abstacts;
using EconomyMonitor.Mapping.AutoMapper;
using EconomyMonitor.Services.UnitOfWork;
using EconomyMonitor.Wpf.MVVM.Abstracts;

namespace EconomyMonitor.Wpf.MVVM.ViewModels.Periods;

/// <summary>
/// Presents add period view model.
/// </summary>
/// <remarks>
/// Inherits <see cref="ViewModelBase"/>.
/// </remarks>
public sealed class AddPeriodViewModel : ViewModelBase, IPeriod
{
    private readonly IPeriodsUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    private DateOnly? _startPeriod;
    private DateOnly? _endPeriod;
    private decimal? _income;

    /// <inheritdoc/>
    public DateOnly StartPeriod
    {
        get => _startPeriod ??= DateOnly.FromDateTime(DateTime.Now);
        set => _ = SetPropertyNotifiable(ref _startPeriod, value);
    }

    /// <inheritdoc/>
    public DateOnly EndPeriod
    {
        get => _endPeriod ??= DateOnly.FromDateTime(DateTime.Now);
        set => _ = SetPropertyNotifiable(ref _endPeriod, value);
    }

    /// <inheritdoc/>
    public decimal Income
    {
        get => _income ??= decimal.Zero;
        set => _ = SetPropertyNotifiable(ref _income, value);
    }

    public AddPeriodViewModel(IPeriodsUnitOfWork periodsUnitOfWork, IEntityWithDtoMapper mapper)
    {
        _unitOfWork = periodsUnitOfWork;
        _mapper = mapper;
    }
}
