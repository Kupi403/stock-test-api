using System.ComponentModel.DataAnnotations;

namespace TestAPI.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]

        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public string Symbol { get; set; } = String.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "Company name cannot be over 10 characters")]
        public string CompanyName { get; set; } = String.Empty;

        [Required]
        [Range(1, 1e6)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(1e-3, 1e2)]
        public decimal LastDividend { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry cannot be over 10 characters")]
        public string Industry { get; set; } = String.Empty;

        [Required]
        [Range(1, 1e9)]
        public long MarketCap { get; set; }
    }


}
