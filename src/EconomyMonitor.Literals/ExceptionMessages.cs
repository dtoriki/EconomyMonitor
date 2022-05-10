namespace EconomyMonitor.Literals;

/// <summary>
/// Provides <see cref="Exception"/> messages.
/// </summary>
public static class ExceptionMessages
{
    /// <summary>
    /// Gets ARGUMENT_OUT_OF_RANGE_CANT_BE_LOWER message.
    /// </summary>
    public static string ARGUMENT_OUT_OF_RANGE_CANT_BE_LOWER => "The argument with name \"{0}\" can not be lower than \"{1}\".";

    /// <summary>
    /// Gets ARGUMENT_OUT_OF_RANGE_CANT_BE_HIGHER message.
    /// </summary>
    public static string ARGUMENT_OUT_OF_RANGE_CANT_BE_HIGHER => "The argument with name \"{0}\" can not be higher than \"{1}\".";

    /// <summary>
    /// Gets INDEX_CANT_BE_LOWER_TAHN_ZERO message.
    /// </summary>
    public static string INDEX_CANT_BE_LOWER_TAHN_ZERO => "The index of array cannot be lower than zero.";

    /// <summary>
    /// Gets OBSCURE_EXCEPTION_THROWN message.
    /// </summary>
    public static string OBSCURE_EXCEPTION_THROWN => "Obscure exception thrown.";


    /// <summary>
    /// Gets NEED_AT_LEAST_A_MESSAGE message.
    /// </summary>
    public static string NEED_AT_LEAST_A_MESSAGE => "Need at least an exception message.";

    /// <summary>
    /// Gets WRONG_TYPE_RECEIVED message.
    /// </summary>
    public static string WRONG_TYPE_RECEIVED => "Type \"{0}\" one was expected, but type \"{1}\" was received.";

    /// <summary>
    /// Gets SELECTED_ITEM_ALREADY_EXISTS message.
    /// </summary>
    public static string SELECTED_ITEM_ALREADY_EXISTS => "Selected item already exists.";

    /// <summary>
    /// Gets SEQUENCE_HAS_NULL_REFERENCE message.
    /// </summary>
    public static string SEQUENCE_HAS_NULL_REFERENCE => "Sequence \"{0}\" has null reference.";

    /// <summary>
    /// Gets OBJECT_DISPOSED message.
    /// </summary>
    public static string OBJECT_DISPOSED => "Object has disposed.";

    /// <summary>
    /// Gets NULL_REFERENCE message.
    /// </summary>
    public static string NULL_REFERENCE => "The \"{0}\" reference \"{1}\" is null.";
}
