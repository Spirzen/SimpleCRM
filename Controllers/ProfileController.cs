using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SimpleCRM.Controllers
{
    /// <summary>
    /// Контроллер для управления профилем пользователя.
    /// </summary>
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Конструктор контроллера ProfileController.
        /// </summary>
        /// <param name="userManager">Сервис управления пользователями.</param>
        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Отображает страницу профиля пользователя.
        /// </summary>
        /// <returns>Представление с данными текущего пользователя.</returns>
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        /// <summary>
        /// Обновляет данные профиля пользователя.
        /// </summary>
        /// <param name="model">Модель данных пользователя для обновления.</param>
        /// <returns>Перенаправление на страницу профиля в случае успеха или представление с ошибками.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Email; // Email используется как UserName

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("Index", user);
        }

        /// <summary>
        /// Отображает страницу изменения пароля.
        /// </summary>
        /// <returns>Представление для изменения пароля.</returns>
        public IActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Обрабатывает запрос на изменение пароля пользователя.
        /// </summary>
        /// <param name="model">Модель данных для изменения пароля.</param>
        /// <returns>Перенаправление на страницу профиля в случае успеха или представление с ошибками.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (changePasswordResult.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }

    /// <summary>
    /// Модель данных для изменения пароля.
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Текущий пароль пользователя.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        /// <summary>
        /// Новый пароль пользователя.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Пароль должен содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Подтверждение нового пароля.
        /// </summary>
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}