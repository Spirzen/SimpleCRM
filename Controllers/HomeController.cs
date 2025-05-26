using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Models;
using System.Diagnostics;

namespace SimpleCRM.Controllers
{
    /// <summary>
    /// Контроллер для управления домашней страницей и другими общими действиями.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Конструктор контроллера HomeController.
        /// </summary>
        /// <param name="logger">Сервис логирования.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Отображает главную страницу приложения.
        /// </summary>
        /// <returns>Представление главной страницы.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Отображает страницу политики конфиденциальности.
        /// </summary>
        /// <returns>Представление страницы политики конфиденциальности.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Отображает страницу ошибки.
        /// </summary>
        /// <returns>Представление страницы ошибки с идентификатором запроса.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}