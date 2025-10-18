namespace LoanAppService.Api.Dtos
{
    public class LoanCreateDto
    {
        public string Number { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int TermValue { get; set; }
        public decimal InterestValue { get; set; }
    }
}
