namespace EconomyMonitor.Helpers.Internals;

internal static class ExceptionMessages
{
    public static string ARGUMENT_OUT_OF_RANGE_CANT_BE_LOWER => "The argument with name \"{0}\" can not be lower than \"{1}\".";
    public static string ARGUMENT_OUT_OF_RANGE_CANT_BE_HIGHER => "The argument with name \"{0}\" can not be higher than \"{1}\".";
    public static string INDEX_CANT_BE_LOWER_TAHN_ZERO => "The index of array cannot be lower than zero.";
    public static string OBSCURE_EXCEPTION_THROWN => "Obscure exception thrown.";
    public static string NEED_AT_LEAST_A_MESSAGE => "Need at least an exception message.";
}
