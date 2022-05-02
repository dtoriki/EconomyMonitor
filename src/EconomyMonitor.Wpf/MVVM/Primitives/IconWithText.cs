using MaterialDesignThemes.Wpf;

namespace EconomyMonitor.Wpf.MVVM.Primitives;

/// <summary>
/// Encapsulates within itself <see cref="PackIconKind"/> with text.
/// </summary>
public sealed class IconWithText
{
    private PackIconKind? _icon;
    private string? _text;

    /// <summary>
    /// Gets or sets icon.
    /// </summary>
    public PackIconKind Icon 
    { 
        get => _icon ??= PackIconKind.None;
        set => _icon = value;
    }

    /// <summary>
    /// Gets or sets text.
    /// </summary>
    public string Text 
    { 
        get => _text ??= string.Empty;
        set => _text = value;
    }

    /// <summary>
    /// Creates <see cref="IconWithText"/> exemplar.
    /// </summary>
    public IconWithText()
    {

    }
}
