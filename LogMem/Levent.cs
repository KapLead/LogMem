namespace LogMem
{
    /// <summary> Тип события лога </summary>
    public enum Levent
    {
        /// <summary> Нет обозначения </summary>
        None,
        /// <summary> Обычное сообщение </summary>
        Default,
        /// <summary> Информационное сообщение </summary>
        Info,
        /// <summary> Предупреждение </summary>
        Warning,
        /// <summary> Ошибка </summary>
        Error,
        /// <summary> Фатальная ошибка (разрушение) </summary>
        Fatal,
        /// <summary> Диагностическое сообщение </summary>
        Diagnostics,
        /// <summary> Сообщение от/для разработчика </summary>
        Develop,
        /// <summary> Баг/Глюк/Иное сообщение возникшее в неожидаемом месте </summary>
        Bug
    }
}
