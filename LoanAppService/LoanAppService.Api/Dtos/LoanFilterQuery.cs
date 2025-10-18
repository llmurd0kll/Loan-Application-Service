using LoanAppService.Api.Domain;

namespace LoanAppService.Api.Dtos
{
    public class LoanFilterQuery
    {
        public LoanStatus? Status { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public int? MinTerm { get; set; }
        public int? MaxTerm { get; set; }
    }
}
