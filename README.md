# Loan Application Service

Тестовое задание: сервис заявок на займ.  
Полный стек: **.NET 8 Web API + EF Core + PostgreSQL + Vue 3 (Element Plus) + Docker Compose**.

---

## 🚀 Возможности

- Создание новой заявки на займ
- Просмотр списка заявок
- Фильтрация по статусу, сумме и сроку
- Смена статуса заявки (опубликовать / снять с публикации)
- Валидация данных на фронтенде и бэкенде
- Полная контейнеризация (Postgres + Backend + Frontend)

---

## 🛠 Технологический стек

- **Backend**: .NET 8 Web API, Entity Framework Core, PostgreSQL
- **Frontend**: Vue 3 (Composition API), Element Plus, Axios, Vite
- **Database**: PostgreSQL 18
- **Containerization**: Docker, Docker Compose
- **UI**: Element Plus

---

## 📦 Запуск проекта

### 1. Клонировать репозиторий
```bash
git clone https://github.com/<your-username>/Loan-Application-Service.git
cd Loan-Application-Service/LoanAppService
```
### 2. Запустить через Docker Compose
```bash
docker-compose up --build -d
```

### 3. Проверка сервисов
- Frontend (Vue + Nginx) → http://localhost:5173
- Backend (Swagger UI) → http://localhost:7117/swagger

## 📋 API эндпоинты
- GET /api/loans — список заявок (с фильтрацией)
- POST /api/loans — создание новой заявки
- PATCH /api/loans/{id}/toggle-status — смена статуса заявки

## 🖥 Интерфейс
- / — список заявок с фильтрами и кнопкой смены статуса
- /create — форма создания новой заявки

## ⚙️ Переменные окружения
В docker-compose.yml заданы:
```yaml
POSTGRES_USER=appuser
POSTGRES_PASSWORD=secret
POSTGRES_DB=loansdb
```

## ✅ Как проверить
- Перейти на http://localhost:5173/create и создать заявку.
- Перейти на http://localhost:5173/ и убедиться, что заявка появилась в списке.
- В Swagger выполнить GET /api/loans и проверить, что данные совпадают.
- В PostgreSQL:
```sql
SELECT * FROM "Loans";
```
## 📂 Структура проекта
```
LoanAppService/
 ├── LoanAppService.Api/        # Backend (.NET 8 Web API)
 ├── frontend/loan-frontend/    # Frontend (Vue 3 + Element Plus)
 ├── docker-compose.yml         # Docker Compose конфигурация
 └── README.md                  # Документация
```
