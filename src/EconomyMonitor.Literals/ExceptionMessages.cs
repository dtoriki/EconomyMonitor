namespace EconomyMonitor.Literals;

/// <summary>
/// Содержит сообщения для вызова исключений <see cref="Exception"/>.
/// </summary>
public static class ExceptionMessages
{
    /// <summary>
    /// Возвращает сообщение "Аргумент \"{0}\" не может быть меньше чем \"{1}\".".
    /// </summary>
    public const string ARGUMENT_OUT_OF_RANGE_CANT_BE_LOWER = "Аргумент \"{0}\" не может быть меньше чем \"{1}\".";

    /// <summary>
    /// Возвращает сообщение "Аргумент \"{0}\" не может быть больше чем \"{1}\".".
    /// </summary>
    public const string ARGUMENT_OUT_OF_RANGE_CANT_BE_HIGHER = "Аргумент \"{0}\" не может быть больше чем \"{1}\".";

    /// <summary>
    /// Возвращает сообщение "Индекс не может быть меньше нуля.".
    /// </summary>
    public const string INDEX_CANT_BE_LOWER_TAHN_ZERO = "Индекс не может быть меньше нуля.";

    /// <summary>
    /// Возвращает сообщение "Было вызвано неизвестное исключение.".
    /// </summary>
    public const string OBSCURE_EXCEPTION_THROWN = "Было вызвано неизвестное исключение.";

    /// <summary>
    /// Возвращает сообщение "Необходимо, хотябы, сообщение об ошибке.".
    /// </summary>
    public const string NEED_AT_LEAST_A_MESSAGE = "Необходимо, хотябы, сообщение об ошибке.";

    /// <summary>
    /// Возвращает сообщение "Ожидался тип \"{0}\", вместо него оказался тип \"{1}\".".
    /// </summary>
    public const string WRONG_TYPE_RECEIVED = "Ожидался тип \"{0}\", вместо него оказался тип \"{1}\".";

    /// <summary>
    /// Возвращает сообщение "Выбранный элемент уже существует.".
    /// </summary>
    public const string SELECTED_ITEM_ALREADY_EXISTS = "Выбранный элемент уже существует.";

    /// <summary>
    /// Возвращает сообщение "Последовательность \"{0}\" содержит пустые ссылки.".
    /// </summary>
    public const string SEQUENCE_HAS_NULL_REFERENCE = "Последовательность \"{0}\" содержит пустые ссылки.";

    /// <summary>
    /// Возвращает сообщение "Объект \"{0}\" типа \"{1}\" был освобождён.".
    /// </summary>
    public const string OBJECT_DISPOSED = "Объект \"{0}\" типа \"{1}\" был освобождён.";

    /// <summary>
    /// Возвращает сообщение "Объект \"{0}\" типа \"{1}\" оказался null.".
    /// </summary>
    public const string NULL_REFERENCE = "Объект \"{0}\" типа \"{1}\" оказался null.";

    /// <summary>
    /// Возвращает сообщение "Не удалось найти строку подключения к хранилищу данных.".
    /// </summary>
    public const string CONNECTION_STRING_WAS_NOT_FOUND = "Не удалось найти строку подключения к хранилищу данных.";

    /// <summary>
    /// Возвращает сообщение "Не удалось получить свойства для изменения.".
    /// </summary>
    public const string PROPERTIES_FOR_SET_NOT_FOUND = "Не удалось получить свойства для изменения.";

    /// <summary>
    /// Возвращает сообщение "Свойство "{0}" только для чтения.".
    /// </summary>
    public const string READ_ONLY_PROPERTY = "Свойство \"{0}\" только для чтения.";

    /// <summary>
    /// Возвращает сообщение "Отсутствует свойство "{0}".".
    /// </summary>
    public const string PROPERTY_MISSED = "Отсутствует свойство \"{0}\".";
}
