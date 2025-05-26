# SimpleCRM
SimpleCRM — это простая система управления обращениями (CRM), разработанная на основе ASP.NET. 
Она предоставляет пользователям возможность регистрироваться, авторизовываться, создавать, просматривать и редактировать обращения. 
Система поддерживает роли пользователей (например, "Администратор" и "Пользователь"), что позволяет гибко управлять доступом к функционалу.

# Возможности
- регистрация и авторизация пользователей (с поддержкой ролей);
- редактирование профиля (имя, email, пароль);
- создание, редактирование и удаление обращений;
- назначение ответственного сотрудника за обращение;
- административная панель.

# Стек
C#, ASP.NET, PostgreSQL, Entity Framework Core, Identity Framework, HTML, CSS, JavaScript, Bootstrap, Razor Pages, AutoMapper, FluentValidation, Logging

# Установка
1. Требуется .NET SDK и PostgreSQL
2. Создайте базу данных в PostgreSQL
3. Настройте строку подключения в файле appsettings.json:
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=SimpleCRM;Username=your_username;Password=your_password"
}
4. Выполните команды для создания и применения миграций:
dotnet ef migrations add InitialCreate
dotnet ef database update
5. Запустите приложение.