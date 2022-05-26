using MaterialDesignThemes.Wpf;
using static EconomyMonitor.Helpers.SetHelper;

namespace EconomyMonitor.Wpf.MVVM.Primitives;

internal sealed class IconWithText
{
    private PackIconKind? _icon;
    private string? _text;

    public PackIconKind Icon 
    { 
        get => _icon ??= PackIconKind.None;
        set => _ = NullCheckSet(ref _icon, value);
    }

    public string Text 
    { 
        get => _text ??= string.Empty;
        set => _ = NullCheckSet(ref _text, value);
    }

    public IconWithText()
    {

    }
}
