namespace SimpleCRM.Models
{
    /// <summary>
    /// Перечисление возможных статусов обращения.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Новое обращение.
        /// </summary>
        New,

        /// <summary>
        /// Обращение в работе.
        /// </summary>
        InProgress,

        /// <summary>
        /// Обращение завершено.
        /// </summary>
        Completed
    }
}