using System;

namespace LoanAppService.Api.Domain
{
    public class Loan
    {
        public int Id { get; set; }                     // PK
        public string Number { get; set; } = string.Empty; // Уникальный номер
        public decimal Amount { get; set; }             // Сумма займа
        public int TermValue { get; set; }              // Срок займа
        public decimal InterestValue { get; set; }      // Процентная ставка
        public LoanStatus Status { get; set; } = LoanStatus.Published; // По умолчанию Published
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset ModifiedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
