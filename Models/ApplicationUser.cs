using Microsoft.AspNetCore.Identity;

namespace SimpleCRM.Models
{
    /// <summary>
    /// Модель пользователя приложения.
    /// Расширяет стандартную модель IdentityUser.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Полное имя пользователя.
        /// Если значение не задано, используется "Пользователь".
        /// </summary>
        public string FullName { get; set; } = "Пользователь";
    }
}