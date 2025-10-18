using LoanAppService.Api.Data;
using LoanAppService.Api.Domain;
using LoanAppService.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanAppService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly AppDbContext _db;

        public LoansController(AppDbContext db)
        {
            _db = db;
        }

        // -------------------------------
        // 1. Получение списка всех заявок
        // -------------------------------
        // GET /api/loans?status=Published&minAmount=100&maxAmount=5000&minTerm=10&maxTerm=60
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetAll([FromQuery] LoanFilterQuery filter)
        {
            IQueryable<Loan> query = _db.Loans.AsNoTracking().OrderByDescending(l => l.CreatedAt);

            // Фильтрация по статусу
            if (filter.Status.HasValue)
                query = query.Where(l => l.Status == filter.Status.Value);

            // Фильтрация по сумме
            if (filter.MinAmount.HasValue)
                query = query.Where(l => l.Amount >= filter.MinAmount.Value);
            if (filter.MaxAmount.HasValue)
                query = query.Where(l => l.Amount <= filter.MaxAmount.Value);

            // Фильтрация по сроку
            if (filter.MinTerm.HasValue)
                query = query.Where(l => l.TermValue >= filter.MinTerm.Value);
            if (filter.MaxTerm.HasValue)
                query = query.Where(l => l.TermValue <= filter.MaxTerm.Value);

            var items = await query
                .Select(l => new LoanDto
                {
                    Id = l.Id,
                    Number = l.Number,
                    Amount = l.Amount,
                    TermValue = l.TermValue,
                    InterestValue = l.InterestValue,
                    Status = l.Status,
                    CreatedAt = l.CreatedAt,
                    ModifiedAt = l.ModifiedAt
                })
                .ToListAsync();

            return Ok(items);
        }

        // -------------------------------
        // 2. Добавление новой заявки
        // -------------------------------
        // POST /api/loans
        [HttpPost]
        public async Task<ActionResult<LoanDto>> Create([FromBody] LoanCreateDto dto)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(dto.Number))
                return BadRequest(new { error = "Номер заявки не должен быть пустым." });
            if (dto.Amount <= 0)
                return BadRequest(new { error = "Сумма должна быть больше 0." });
            if (dto.TermValue <= 0)
                return BadRequest(new { error = "Срок займа должен быть больше 0." });
            if (dto.InterestValue <= 0)
                return BadRequest(new { error = "Процентная ставка должна быть больше 0." });

            // Проверка уникальности номера
            var exists = await _db.Loans.AnyAsync(x => x.Number == dto.Number);
            if (exists)
                return Conflict(new { error = "Заявка с таким номером уже существует." });

            var now = DateTimeOffset.UtcNow;

            var loan = new Loan
            {
                Number = dto.Number.Trim(),
                Amount = dto.Amount,
                TermValue = dto.TermValue,
                InterestValue = dto.InterestValue,
                Status = LoanStatus.Published,   // По ТЗ: Published при создании
                CreatedAt = now,
                ModifiedAt = now
            };

            _db.Loans.Add(loan);
            await _db.SaveChangesAsync();

            var result = new LoanDto
            {
                Id = loan.Id,
                Number = loan.Number,
                Amount = loan.Amount,
                TermValue = loan.TermValue,
                InterestValue = loan.InterestValue,
                Status = loan.Status,
                CreatedAt = loan.CreatedAt,
                ModifiedAt = loan.ModifiedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, result);
        }

        // -------------------------------
        // 3. Получение заявки по Id
        // -------------------------------
        // GET /api/loans/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LoanDto>> GetById([FromRoute] int id)
        {
            var l = await _db.Loans.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (l == null) return NotFound();

            return Ok(new LoanDto
            {
                Id = l.Id,
                Number = l.Number,
                Amount = l.Amount,
                TermValue = l.TermValue,
                InterestValue = l.InterestValue,
                Status = l.Status,
                CreatedAt = l.CreatedAt,
                ModifiedAt = l.ModifiedAt
            });
        }

        // -------------------------------
        // 4. Снятие/возврат заявки на публикацию
        // -------------------------------
        // PATCH /api/loans/{id}/toggle-status
        [HttpPatch("{id:int}/toggle-status")]
        public async Task<ActionResult<LoanDto>> ToggleStatus([FromRoute] int id)
        {
            var loan = await _db.Loans.FirstOrDefaultAsync(x => x.Id == id);
            if (loan == null) return NotFound();

            // Переключение статуса
            loan.Status = loan.Status == LoanStatus.Published
                ? LoanStatus.Unpublished
                : LoanStatus.Published;

            loan.ModifiedAt = DateTimeOffset.UtcNow;

            await _db.SaveChangesAsync();

            var result = new LoanDto
            {
                Id = loan.Id,
                Number = loan.Number,
                Amount = loan.Amount,
                TermValue = loan.TermValue,
                InterestValue = loan.InterestValue,
                Status = loan.Status,
                CreatedAt = loan.CreatedAt,
                ModifiedAt = loan.ModifiedAt
            };

            return Ok(result);
        }
    }
}
