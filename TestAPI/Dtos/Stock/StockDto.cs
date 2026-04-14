using System.ComponentModel.DataAnnotations.Schema;
using TestAPI.Dtos.Comment;

namespace TestAPI.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = String.Empty;
        public string CompanyName { get; set; } = String.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDividend { get; set; }
        public string Industry { get; set; } = String.Empty;

        public long MarketCap { get; set; }

        public List<CommentDto> Comments { get; set; }


    }
}
