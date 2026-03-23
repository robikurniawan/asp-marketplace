# AspMarketplace — Project Context

## Deskripsi
Platform marketplace berbasis ASP.NET Core 10 MVC dengan area-based routing.

## Tech Stack
| Layer | Teknologi |
|---|---|
| Framework | ASP.NET Core 10 MVC |
| Database | MariaDB via Pomelo EF Core |
| ORM | Entity Framework Core 10 + snake_case naming |
| Validasi | FluentValidation |
| Logging | Serilog |
routing manual tanpa mengikuti pages controller


## Area & URL Pattern
| Area | Base URL | Keterangan |
|---|---|---|
| Public | `/` | Halaman publik, tidak perlu login |
| Auth | `/auth/...` | Login, logout |
| Admin | `/admin/...` | Panel admin, perlu login |

## Pola yang Digunakan
- Repository Pattern dengan generic `IRepository<T>`
- Service Layer terpisah dari Controller
- Soft delete via `IsDeleted` flag di `BaseEntity`
- `ICurrentUserService` untuk akses user context
- `ApiResponse<T>` untuk semua JSON response
- `TempData` untuk pesan sukses/error antar redirect

## Struktur Folder Utama
```
AspMarketplace/
├── Areas/
│   ├── Admin/    → Controllers, Views
│   ├── Auth/     → Controllers, Views
│   └── Public/   → Controllers, Views
├── Controllers/  → BaseController
├── Models/       → BaseEntity + semua entity
├── Interfaces/   → semua interface
├── Services/     → implementasi service
├── Repositories/ → implementasi repository
├── DTOs/         → ApiResponse, PaginatedResult
├── ViewModels/   → ViewModel per fitur
├── Data/         → AppDbContext, Migrations
├── Extensions/   → ServiceExtensions (DI registration)
└── Middleware/   → ExceptionMiddleware
```