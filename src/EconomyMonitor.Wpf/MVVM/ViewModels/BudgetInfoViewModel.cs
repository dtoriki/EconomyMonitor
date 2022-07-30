using System.Globalization;
using System.Windows.Input;
using EconomyMonitor.Services;
using EconomyMonitor.Wpf.MVVM.Commands;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.DisposeHelper;
using static EconomyMonitor.Helpers.SetHelper;
using static EconomyMonitor.Literals.Literals;

namespace EconomyMonitor.Wpf.MVVM.ViewModels;

internal sealed class BudgetInfoViewModel : NotifyPropertyChangedBase
{
    private readonly IAsyncCommand _loadBudgetCommand;
    private readonly IAsyncCommand _saveBudgetCommand;
    private readonly ICommand _startEditBudgetCommand;
    private readonly ICommand _cancelEditBudgetCommand;
    private readonly IServiceScopeFactory _scopeFactory;

    private decimal _budget;
    private string? _budgetAsString;
    private bool _isEditMode;

    public IAsyncCommand LoadBudgetCommand => _loadBudgetCommand;
    public IAsyncCommand SaveBudgetCommand => _saveBudgetCommand;
    public ICommand StartEditBudgetCommand => _startEditBudgetCommand;
    public ICommand CancelEditBudgetCommand => _cancelEditBudgetCommand;

    public string? BudgetAsString
    {
        get => _budgetAsString ??= ZERO_LITERAL;
        set => _ = SetPropertyNotifiable(ref _budgetAsString, value);
    }
    public decimal Budget => _budget;
    public bool IsEditMode => _isEditMode;

    public BudgetInfoViewModel(IServiceScopeFactory scopeFactory)
    {
        _loadBudgetCommand = new RelayAsyncCommand(ExecuteLoadBudgetAsync);
        _saveBudgetCommand = new RelayAsyncCommand(ExecuteSaveBudgetAsync);
        _startEditBudgetCommand = new RelayCommand(ExecuteStartEditBudget, CanExecuteStartEditBudget);
        _cancelEditBudgetCommand = new RelayCommand(ExecuteCancelEditBudget);

        _scopeFactory = scopeFactory;

        _budgetAsString = string.Empty;
        _budget = decimal.Zero;
    }

    private async Task ExecuteLoadBudgetAsync(object? parameter, CancellationToken cancellationToken)
    {
        await using AsyncServiceScope asyncScope = _scopeFactory.CreateAsyncScope();
        IBudgetService budgetService = asyncScope.ServiceProvider.GetRequiredService<IBudgetService>();

        decimal budget = await budgetService.GetBudgetAsync(cancellationToken);
        _ = SetPropertyNotifiable(ref _budget, budget, nameof(Budget));
        BudgetAsString = budget.ToString();

        _ = await DisposeObjectAsync(budgetService);
    }

    private void ExecuteStartEditBudget(object? parameter) => _ = SetPropertyNotifiable(ref _isEditMode, true, nameof(IsEditMode));
    private bool CanExecuteStartEditBudget(object? parameter) => !_saveBudgetCommand.Execution.IsInProgress;
    private void ExecuteCancelEditBudget(object? parameter)
    {
        BudgetAsString = _budget.ToString();

        _ = SetPropertyNotifiable(ref _isEditMode, value: false, nameof(IsEditMode));
    }

    private async Task ExecuteSaveBudgetAsync(object? parameter, CancellationToken cancellationToken)
    {
        _ = SetPropertyNotifiable(ref _isEditMode, value: false, nameof(IsEditMode));
        await using AsyncServiceScope asyncScope = _scopeFactory.CreateAsyncScope();
        IBudgetService budgetService = asyncScope.ServiceProvider.GetRequiredService<IBudgetService>();

        decimal editedBudget = Convert.ToDecimal(
            BudgetAsString?.Trim().Replace(",", "."), 
            CultureInfo.InvariantCulture);

        if (_budget == editedBudget)
        {
            return;
        }

        await budgetService.SetBudgetAsync(editedBudget, cancellationToken);
        _ = SetPropertyNotifiable(ref _budget, editedBudget, nameof(Budget));
    }
}
