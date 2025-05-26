using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Data;
using SimpleCRM.Models;

namespace SimpleCRM.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Requests
        public IActionResult Index(string statusFilter, string sortOrder)
        {
            ViewData["DateSortParam"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["StatusSortParam"] = sortOrder == "status_asc" ? "status_desc" : "status_asc";

            var requests = _context.Requests.AsQueryable();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                requests = requests.Where(r => r.Status == statusFilter); // Фильтр по тексту
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

        // GET: Requests/Details/5
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

        // GET: Requests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Requests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,ResponsibleEmployee")] Request request)
        {
            if (ModelState.IsValid)
            {
                _context.Add(request); // EF Core автоматически использует значение CreatedAt из модели
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: Requests/Edit/5
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

        // POST: Requests/Edit/5
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

        // GET: Requests/Delete/5
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

        // POST: Requests/Delete/5
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