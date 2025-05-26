using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleCRM.Models;

namespace SimpleCRM.Data
{
    /// <summary>
    /// Контекст базы данных приложения.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Конструктор контекста базы данных.
        /// </summary>
        /// <param name="options">Параметры конфигурации для контекста.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Набор данных для обращений.
        /// </summary>
        public DbSet<Request> Requests { get; set; }
    }
}