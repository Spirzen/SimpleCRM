namespace SimpleCRM.Models
{
    /// <summary>
    /// Модель данных для страницы ошибки.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Идентификатор запроса, вызвавшего ошибку.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Определяет, следует ли отображать идентификатор запроса.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}