using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleCRM.Data;
using SimpleCRM.Models;

/// <summary>
/// Точка входа приложения.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Добавление сервисов в контейнер.
/// </summary>
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Регистрация контекста базы данных с PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Настройка Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI(); // Добавляет поддержку UI для Identity

// Настройка маршрутов для Identity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login"; // Маршрут для страницы входа
    options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Маршрут для страницы "Доступ запрещен"
});

// Добавление аутентификации и авторизации
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

/// <summary>
/// Обновление данных пользователей при старте приложения.
/// </summary>
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Обновляем данные администратора
    var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    if (adminUser != null)
    {
        adminUser.FullName = "Администратор";
        await userManager.UpdateAsync(adminUser);
    }

    // Обновляем данные обычного пользователя
    var normalUser = await userManager.FindByEmailAsync("user@example.com");
    if (normalUser != null)
    {
        normalUser.FullName = "Пользователь";
        await userManager.UpdateAsync(normalUser);
    }
}

/// <summary>
/// Настройка конвейера HTTP-запросов.
/// </summary>
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Включение аутентификации и авторизации
app.UseAuthentication();
app.UseAuthorization();

// Настройка маршрутов контроллеров
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Requests}/{action=Index}/{id?}");

// Поддержка Razor Pages
app.MapRazorPages();

app.Run();