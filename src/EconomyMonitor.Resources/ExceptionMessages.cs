namespace EconomyMonitor.Resources;

/// <summary>
/// Messages patterns for exceptions.
/// </summary>
public static class ExceptionMessages
{
    /// <summary>
    /// Gets ARGUMENT_OUT_OF_RANGE_CANT_BE_LOWER pattern.
    /// </summary>
    public static string ARGUMENT_OUT_OF_RANGE_CANT_BE_LOWER => "The argument with name \"{0}\" can not be lower than \"{1}\".";

    /// <summary>
    /// Gets ARGUMENT_OUT_OF_RANGE_CANT_BE_HIGHER pattern.
    /// </summary>
    public static string ARGUMENT_OUT_OF_RANGE_CANT_BE_HIGHER => "The argument with name \"{0}\" can not be higher than \"{1}\".";

    /// <summary>
    /// Gets INDEX_CANT_BE_LOWER_TAHN_ZERO pattern.
    /// </summary>
    public static string INDEX_CANT_BE_LOWER_TAHN_ZERO => "The index of array cannot be lower than zero.";

    /// <summary>
    /// Gets OBSCURE_EXCEPTION_THROWN pattern.
    /// </summary>
    public static string OBSCURE_EXCEPTION_THROWN => "Obscure exception thrown.";

    /// <summary>
    /// Gets NEED_AT_LEAST_A_MESSAGE pattern.
    /// </summary>
    public static string NEED_AT_LEAST_A_MESSAGE => "Need at least an exception message.";

    /// <summary>
    /// Gets WRONG_TYPE_RECEIVED pattern.
    /// </summary>
    public static string WRONG_TYPE_RECEIVED => "Type \"{0}\" one was expected, but type \"{1}\" was received";
}
