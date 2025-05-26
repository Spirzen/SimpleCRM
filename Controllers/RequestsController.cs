using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Data;
using SimpleCRM.Models;

namespace SimpleCRM.Controllers
{
    /// <summary>
    /// Контроллер для управления обращениями.
    /// </summary>
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера RequestsController.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отображает список всех обращений с возможностью фильтрации и сортировки.
        /// </summary>
        /// <param name="statusFilter">Фильтр по статусу обращения.</param>
        /// <param name="sortOrder">Параметр сортировки (по дате или статусу).</param>
        /// <returns>Представление со списком обращений.</returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Index(string statusFilter, string sortOrder)
        {
            ViewData["DateSortParam"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["StatusSortParam"] = sortOrder == "status_asc" ? "status_desc" : "status_asc";

            var requests = _context.Requests.AsQueryable();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                requests = requests.Where(r => r.Status == statusFilter);
            }

            switch (sortOrder)
            {
                case "date_asc":
                    requests = requests.OrderBy(r => r.CreatedAt);
                    break;
                case "date_desc":
                    requests = requests.OrderByDescending(r => r.CreatedAt);
                    break;
                case "status_asc":
                    requests = requests.OrderBy(r => r.Status);
                    break;
                case "status_desc":
                    requests = requests.OrderByDescending(r => r.Status);
                    break;
            }

            ViewBag.StatusFilter = statusFilter;

            return View(requests.ToList());
        }

        /// <summary>
        /// Отображает детали выбранного обращения.
        /// </summary>
        /// <param name="id">Идентификатор обращения.</param>
        /// <returns>Представление с деталями обращения или страница ошибки, если обращение не найдено.</returns>
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = _context.Requests.FirstOrDefault(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        /// <summary>
        /// Отображает форму для создания нового обращения.
        /// </summary>
        /// <returns>Представление формы создания обращения.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Обрабатывает создание нового обращения.
        /// </summary>
        /// <param name="request">Модель данных нового обращения.</param>
        /// <returns>Перенаправление на список обращений в случае успеха или представление с ошибками.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,ResponsibleEmployee")] Request request)
        {
            if (ModelState.IsValid)
            {
                _context.Add(request);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        /// <summary>
        /// Отображает форму для редактирования существующего обращения.
        /// </summary>
        /// <param name="id">Идентификатор обращения.</param>
        /// <returns>Представление формы редактирования или страница ошибки, если обращение не найдено.</returns>
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = _context.Requests.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        /// <summary>
        /// Обрабатывает обновление существующего обращения.
        /// </summary>
        /// <param name="id">Идентификатор обращения.</param>
        /// <param name="request">Обновленная модель данных обращения.</param>
        /// <returns>Перенаправление на список обращений в случае успеха или представление с ошибками.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Description,Status,ResponsibleEmployee")] Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(request);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        /// <summary>
        /// Отображает форму подтверждения удаления обращения.
        /// </summary>
        /// <param name="id">Идентификатор обращения.</param>
        /// <returns>Представление формы подтверждения удаления или страница ошибки, если обращение не найдено.</returns>
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = _context.Requests.FirstOrDefault(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        /// <summary>
        /// Обрабатывает удаление обращения.
        /// </summary>
        /// <param name="id">Идентификатор обращения.</param>
        /// <returns>Перенаправление на список обращений.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var request = _context.Requests.Find(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}