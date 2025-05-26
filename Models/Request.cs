using System.ComponentModel.DataAnnotations;

namespace SimpleCRM.Models
{
    /// <summary>
    /// Модель обращения в системе.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Уникальный идентификатор обращения.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Дата и время создания обращения.
        /// Значение по умолчанию: текущее время (UTC).
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Заголовок обращения.
        /// Обязательное поле, максимальная длина — 255 символов.
        /// </summary>
        [Required]
        [StringLength(255)]
        public required string Title { get; set; }

        /// <summary>
        /// Описание обращения.
        /// Обязательное поле.
        /// </summary>
        [Required]
        public required string Description { get; set; }

        /// <summary>
        /// Статус обращения.
        /// Значение по умолчанию: "Новое".
        /// </summary>
        [Required]
        public string Status { get; set; } = "Новое";

        /// <summary>
        /// Ответственный сотрудник за обращение.
        /// Необязательное поле.
        /// </summary>
        public string? ResponsibleEmployee { get; set; }
    }
}